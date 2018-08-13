using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.DtoModels;
using ExpenseManagerBackEnd.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace ExpenseManagerBackEnd.Controllers {
   
    [Route("/api/category"),Authorize]
    public class CategoryController:ControllerBase {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        
        public CategoryController(ICategoryRepository categoryRepository, IUserRepository userRepository) {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;

        }

        [HttpGet("{userid?}")]
        public async Task<IActionResult> GetCategories([FromRoute] string userid) {
            try {

                List<ExpenseCategory> expenseCategories; 
                if (string.IsNullOrEmpty(userid)) {
                  expenseCategories =  await  _categoryRepository.GetCategoriesDefault();
                }
                else {
                    expenseCategories = await _categoryRepository.GetCategoriesByUser(userid);
                }
                Console.WriteLine("Categories List === " + expenseCategories);
                return Ok(expenseCategories);
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                return BadRequest(new ErrorModel<object>(ProjectCodes.Generic_Error));
            }


        }
        
        
        
    }
}