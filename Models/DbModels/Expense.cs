using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagerBackEnd.Models.DbModels
{
    
    public enum PaymentMethod
    {
        Cash =0,
        Check,
        CreditCard,
        DebitCard
    }
    
    public class Expense
    {
        
        public int ExpenseId { get; set; }
        
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        
        public string UserId { get; set; }
        
        public string ExpenseCategoryId { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }     
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM--dd}")]
        public DateTime ExpenseDate { get; set; }
        
        
        public User User { get; set; }
        
        public ExpenseCategory ExpenseCategory { get; set; }
        
        
    }
}