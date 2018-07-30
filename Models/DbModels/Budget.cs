using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagerBackEnd.Models.DbModels
{
    public class Budget
    {
        public int BudgetId { get; set; }
        
        [Column(TypeName = "money")]
        public decimal Money { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM--dd}",ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM--dd}",ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }
        
        public int UserId { get; set; }
        
        public User User { get; set; }
        

    }
}