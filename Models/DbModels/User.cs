using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;

namespace ExpenseManagerBackEnd.Models.DbModels
{
    public class User
    {
        
        public string UserId { get; set; }
        
        [Required]public string Name { get; set; }
        
        [Required][EmailAddress]public  string Email { get; set; }
        
        [Phone]public string Phone { get; set; }
        
        [DataType(DataType.Password)]public string Password { get; set; }
        
        public ICollection<ExpenseCategory> ExpenseCategories { get; set; }
    }
}