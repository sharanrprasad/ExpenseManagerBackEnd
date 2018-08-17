using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Models.DbModels;

namespace ExpenseManagerBackEnd.Contracts {
    public interface ICategoryRepository  {
        Task<List<ExpenseCategory>> GetCategoriesByUser(string userId);
        Task<List<ExpenseCategory>> GetCategoriesDefault();
        Task<ExpenseCategory> AddParentCategory(ExpenseCategory expenseCategory);
    }

    
}