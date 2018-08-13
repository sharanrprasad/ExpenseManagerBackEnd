using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.DtoModels;
using ExpenseManagerBackEnd.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExpenseManagerBackEnd.Controllers {

    [Route("api/summary")]
    [Produces("application/json")]
    public class SummaryController : ControllerBase {

        private readonly IExpenseRepository _expenseRepository;
        private readonly IBudgetRepository _budgetRepository;


        public SummaryController(IExpenseRepository expenseRepository, IBudgetRepository budgetRepository) {
            _expenseRepository = expenseRepository;
            _budgetRepository = _budgetRepository;
        }

        [HttpPost("bydate")]
        public async Task<IActionResult> GetSummary([FromBody] GetSummaryModel data) {
            if (data == null || !ModelState.IsValid) {
                return BadRequest(new ErrorModel<object>(ProjectCodes.Form_Generic_Error));
            }

            try {
                data.FromDate = TimeZoneInfo.ConvertTimeToUtc(data.FromDate);
                data.ToDate = TimeZoneInfo.ConvertTimeToUtc(data.ToDate);

                var summaryModel = new SummaryModel() {
                    TotalExpenditure = 0,
                    ExpenditureCategoryMap = new Dictionary<string, SpendingCategoryModel>()
                };

                var expenses = await _expenseRepository.GetAllExpenses(data.UserId, data.FromDate, data.ToDate);

                expenses.ForEach(exp => {
                    int categoryId;
                    ExpenseCategory category = null;
                    if (exp.ExpenseCategory.ParentId != null) {
                        categoryId = exp.ExpenseCategory.ParentId.Value;
                        category = exp.ExpenseCategory.ExpenseCategoryParent;
                    }
                    else {
                        categoryId = exp.ExpenseCategory.ExpenseCategoryId;
                        category = exp.ExpenseCategory;
                    }

                    if (summaryModel.ExpenditureCategoryMap.ContainsKey(categoryId.ToString())) {
                        summaryModel.ExpenditureCategoryMap[categoryId.ToString()].CategoryExpenditure += exp.Price;
                    }
                    else {
                        summaryModel.ExpenditureCategoryMap.Add(categoryId.ToString(), new SpendingCategoryModel() {
                            CategoryExpenditure = exp.Price,
                            ExpenseCategory = new ExpenseCategory() {
                                ExpenseCategoryId = category.ExpenseCategoryId,
                                Name = category.Name,
                                ParentId = category.ParentId,
                                ChildCategories = null,
                                UserId = category.UserId
                            }
                        });
                    }

                    summaryModel.TotalExpenditure += exp.Price;
                });

                return Ok(summaryModel);
            }
            catch (Exception e) {
                return BadRequest(new ErrorModel<object>(ProjectCodes.Generic_Error));
            }
        }


    }
}