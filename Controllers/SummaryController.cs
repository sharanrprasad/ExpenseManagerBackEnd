
using System;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagerBackEnd.Controllers {
    
    [Route("/api/summary")]
    [Produces("application/json")]
    public class SummaryController:ControllerBase {

        private readonly IExpenseRepository _expenseRepository;
        private readonly IBudgetRepository _budgetRepository;


        public SummaryController(IExpenseRepository expenseRepository, IBudgetRepository budgetRepository) {
            _expenseRepository = expenseRepository;
            _budgetRepository = _budgetRepository;
        }

        [HttpPost("/bydate")]
        public async Task<IActionResult> GetSummary([FromBody]GetSummaryModel data) {
            if (data == null || !ModelState.IsValid) {
                return BadRequest(new ErrorModel<object>(ProjectCodes.Form_Generic_Error)); 
            }

            try {
                data.FromDate = TimeZoneInfo.ConvertTimeToUtc(data.FromDate);
                data.ToDate = TimeZoneInfo.ConvertTimeToUtc(data.ToDate);
                var expenses = await _expenseRepository.GetAllExpenses(data.UserId, data.FromDate, data.ToDate);

                var totalExpenditure = 0.0f;

                expenses.ForEach(exp => { });

                return Ok(new SummaryModel() {
                    Expenses = expenses
                });

            }
            catch (Exception e) {
                return BadRequest(new ErrorModel<object>(ProjectCodes.Generic_Error));
            }
            
        }
        
        
        
        
    }
}