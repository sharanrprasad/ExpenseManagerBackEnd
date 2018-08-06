using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseManagerBackEnd.Models.DbModels;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ExpenseManagerBackEnd.Models.ApiModels {
    
    [NotMapped]
    public class LoginModel {
        
        [Required] public string Email{ get; set; }
        [Required] public string Password{ get; set; }
    }

    [NotMapped]
    public class GetSummaryModel {
        [Required] public string UserId{ get; set; }
        [Required] public DateTime FromDate{ get; set; }
        [Required] public DateTime ToDate{ get; set; }
    }

    [NotMapped]
    [Serializable]
    public class SummaryModel {
        public decimal TotalExpenditure{ get; set; }
        public ExpenseCategory MostSpentCategory{ get; set; }
        public Dictionary<string,SpendingCategoryModel> ExpenditureCategoryMap{ get; set; }
        
    }

    [NotMapped]
    public class SpendingCategoryModel {
        public decimal CategoryExpenditure{ get; set; }
        public ExpenseCategory ExpenseCategory{ get; set; }
    }
}