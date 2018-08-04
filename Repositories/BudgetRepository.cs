using System;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagerBackEnd.Repositories {
    public class BudgetRepository : IBudgetRepository {

        private readonly ExpenseManagerContext _context;

        public BudgetRepository(ExpenseManagerContext context) {
            _context = context;
        }

        public async Task<Budget> AddBudget(Budget budget) {
            await _context.Budgets.AddAsync(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<Budget> GetCurrentBudget(string userId) {
            var budget = await _context.Budgets
                .Where(b => (b.UserId == userId && b.FromDate <= DateTime.UtcNow && b.ToDate >= DateTime.UtcNow))
                .SingleOrDefaultAsync();
            return budget;
        }

        public async Task<Budget> DeleteBudget(int budgetId) {
            var budget = await _context.Budgets.SingleAsync(b => b.BudgetId == budgetId);
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<Budget> UpdateBudget(Budget budget) {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<bool> Exists(int id) {
            return await _context.Budgets.AnyAsync(b => b.BudgetId == id);
        }


        public async Task<bool> Exists(string userId, DateTime startTime, DateTime endTime) {
           return await _context.Budgets.Where(b => b.UserId == userId 
                                              && ((startTime >= b.FromDate && startTime <= b.ToDate) 
                                                  || (endTime >= b.FromDate && endTime < b.ToDate)))
                .AnyAsync();
        }
    }
}