using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.DtoModels;
using ExpenseManagerBackEnd.Models.DbModels;
using ExpenseManagerBackEnd.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagerBackEnd.Controllers {

    [Route("api/budget"), Produces("application/json"), Authorize]
    public class BudgetController : ControllerBase {

        private readonly IUserRepository _userRepository;
        private readonly IBudgetRepository _budgetRepository;

        public BudgetController(IUserRepository userRepository, IBudgetRepository budgetRepository) {
            _userRepository = userRepository;
            _budgetRepository = budgetRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBudget([FromBody] Budget budget) {
            if (budget == null || !ModelState.IsValid) {
                return BadRequest(new ErrorModel<object>(ProjectCodes.Form_Generic_Error));
            }

            try {

                budget = TimeUtils.GetBudgetUtc(budget);
                if (await _budgetRepository.Exists(budget.UserId, budget.FromDate, budget.ToDate)) {
                    return BadRequest(new ErrorModel<object>(ProjectCodes.Budget_Already_Present));
                }

                var result = await _budgetRepository.AddBudget(budget);
                return Ok(result);
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                return BadRequest(new ErrorModel<object>(ProjectCodes.Generic_Error));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBudget([FromRoute] int id) {
            if (!await _budgetRepository.Exists(id)) {
                return NotFound();
            }

            try {
                var deletedBudget = await _budgetRepository.DeleteBudget(id);
                return Ok(deletedBudget);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(new ErrorModel<object>(ProjectCodes.Generic_Error));
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateBudget([FromBody] Budget budget) {
            if (!ModelState.IsValid || budget == null) {
                return BadRequest(new ErrorModel<object>(ProjectCodes.Form_Generic_Error));
            }

            try {
                budget = TimeUtils.GetBudgetUtc(budget);
                await _budgetRepository.UpdateBudget(budget);
                return Ok(budget);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(new ErrorModel<object>(ProjectCodes.Generic_Error));
            }
        }


        [HttpGet("current/{userid}")]
        public async Task<IActionResult> GetCurrentBudget([FromRoute] string userid) {
            if (!await _userRepository.Exists(userid)) {
                return BadRequest(new ErrorModel<object>(ProjectCodes.User_Not_Found));
            }

            try {
                var currentBudget = await _budgetRepository.GetCurrentBudget(userid);
                return Ok(currentBudget);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return NotFound();
            }
        }


       


    }
}