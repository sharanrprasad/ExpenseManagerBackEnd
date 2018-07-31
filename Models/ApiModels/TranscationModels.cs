using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagerBackEnd.Models.ApiModels {
    
    [NotMapped]
    public class LoginModel {
        
        [Required] public string Email{ get; set; }
        [Required] public string Password{ get; set; }
    }
}