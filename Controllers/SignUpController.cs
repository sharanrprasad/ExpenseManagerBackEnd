using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.ApiModels;
using ExpenseManagerBackEnd.Models.DbModels;
using ExpenseManagerBackEnd.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagerBackEnd.Controllers {
    [Route("/api/signup")]
    [Produces("application/json")]
    public class SignUpController : ControllerBase {
        private readonly IUserRepository _userRepository;

        public SignUpController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] User user) {
            
            if (user == null || !ModelState.IsValid) {
                return BadRequest(new ErrorModel<Object>(ProjectCodes.Form_Generic_Error));
            }

            var userData = await _userRepository.GetUser(user.Email);

            if (userData != null) {
                return BadRequest(new ErrorModel<User>(ProjectCodes.User_Already_Present, userData));
            }

            try {
                userData = await _userRepository.CreateUser(user);
                return Ok(new {User = userData, Token = TokenUtils.GetNewUserToken(userData.Email, userData.Password)});

            }
            catch (Exception exception) {
                Console.WriteLine(exception.ToString());
                return BadRequest();
            }
        }
    }
}