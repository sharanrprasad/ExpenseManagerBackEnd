using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagerBackEnd.Repositories {
    public class CategoryRepository : ICategoryRepository {

        private readonly ExpenseManagerContext _context;

        public CategoryRepository(ExpenseManagerContext context) {
            _context = context;
        }


        public async Task<List<ExpenseCategory>> GetCategoriesByUser(string userId) {
            var expenseCategoryList = await _context.ExpenseCategories
                .Where(ec => ec.ParentId == null && (ec.UserId == null || ec.UserId == userId))
                .Include(ec => ec.ChildCategories).ToListAsync();
            return expenseCategoryList;
        }

        public async Task<List<ExpenseCategory>> GetCategoriesDefault() {
            var expenseCategoryList = await _context.ExpenseCategories
                .Where(ec => ec.ParentId == null && (ec.UserId == null))
                .Include(ec => ec.ChildCategories).ToListAsync();
            return expenseCategoryList;
        }
    }
}