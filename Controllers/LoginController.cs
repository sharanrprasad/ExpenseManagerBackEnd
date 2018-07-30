using System;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.DbModels;
using ExpenseManagerBackEnd.Models.ApiModels;
using ExpenseManagerBackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagerBackEnd.Controllers {
    [Route("api/login")]
    [Produces("application/json")]
    public class LoginController : ControllerBase {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Produces(typeof(User))]
        public async Task<IActionResult> Post([FromBody] LoginModel loginData) {
            if (loginData == null || !ModelState.IsValid) {
                return BadRequest(new ErrorModel<Object>(ProjectCodes.Form_Generic_Error));
            }

            var userData = await _userRepository.GetUser(loginData.Email);

            if (userData == null) {
                return NotFound();
            }
            else if (userData.Password != loginData.Password) {
                return BadRequest(new ErrorModel<Object>(ProjectCodes.In_Correct_Password));
            }

            return Ok(new {Token = "Token here", User = userData});
        }
    }
}