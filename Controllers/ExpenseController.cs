using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.DtoModels;
using ExpenseManagerBackEnd.Models.DbModels;
using ExpenseManagerBackEnd.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace ExpenseManagerBackEnd.Controllers {
   
    [Route("api/expense"),Produces("application/json"),Authorize]
    public class ExpenseController:ControllerBase {

        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserRepository _userRepository;
        
        public ExpenseController(IExpenseRepository expenseRepository, IUserRepository userRepository) {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }

        [HttpPost("add")]
        [Produces(typeof(Expense))]
        public async  Task<IActionResult> AddExpense([FromBody] Expense expense) {
            
            Console.WriteLine("[Add Expense] " + expense);

            if (expense == null || !ModelState.IsValid) {
                return BadRequest(new ErrorModel<Object>(ProjectCodes.Form_Generic_Error));
            } 
            
            Console.WriteLine("[ExpenseControlelr][Add Expense]"+ expense.ExpenseId);

            try {
                if (string.IsNullOrEmpty(expense.UserId) || ! await _userRepository.Exists(expense.UserId)) {
                    return BadRequest(new ErrorModel<Object>(ProjectCodes.User_Not_Found));
                }

                expense = TimeUtils.GetExpenseUtc(expense);
                await  _expenseRepository.AddExpense(expense);
                return Ok(expense);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(new ErrorModel<Object>(ProjectCodes.Generic_Error));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] int  id) {
            
            if (!ModelState.IsValid) {
                return BadRequest(new ErrorModel<Object>(ProjectCodes.Form_Generic_Error));
            }
            
            Console.WriteLine("[ExpenseControlelr][Delete Expense]"+ id);

            try {

                if ( await _expenseRepository.GetExpense(id) == null) {
                    return NotFound("Expense Not Found");
                }
                await  _expenseRepository.DeleteExpense(id);
                return Ok();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(new ErrorModel<Object>(ProjectCodes.Generic_Error));
            }

        }


        [HttpGet("user/{userid}")]
        public async Task<IActionResult> GetExpense([FromRoute] string userid, [FromHeader]string start_date, [FromHeader] string end_date) {
           
            var isUserPresent = await _userRepository.Exists(userid);
            
            if (!isUserPresent) {
                return BadRequest(new ErrorModel<object>(ProjectCodes.User_Not_Found));
            }
            try {

                List<Expense> expenses;
                
                if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date)) {
                    
                    var startDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(start_date));
                    var endDate =  TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(end_date));
                    
                    expenses  = await _expenseRepository.GetAllExpenses(userid, startDate, endDate);
                }
                else {
                    expenses = await _expenseRepository.GetAllExpenses(userid);
                }

                return Ok(expenses);

            }
            catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(new ErrorModel<Object>(ProjectCodes.Generic_Error));
            }
            
            
        }
        
        
    }
}