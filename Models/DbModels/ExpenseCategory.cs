
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagerBackEnd.Models.DbModels
{
    public class ExpenseCategory
    {
        
        public  int ExpenseCategoryId { get; set; }
        
        public  string Name { get; set; }
        
        public int? ParentId { get; set; }
        
        public string UserId { get; set; }
        
        [ForeignKey("ParentId")]public ExpenseCategory ExpenseCategoryParent{ get; set; }
        
        public virtual ICollection<ExpenseCategory>  ChildCategories{ get; set; }
        
    }
}