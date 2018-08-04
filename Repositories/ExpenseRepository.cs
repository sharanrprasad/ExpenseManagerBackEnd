using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagerBackEnd.Repositories {
    
    public class ExpenseRepository : IExpenseRepository {

        private readonly ExpenseManagerContext _context;

        public ExpenseRepository(ExpenseManagerContext context) {
            _context = context;
        }

        public  async  Task<Expense> AddExpense(Expense expense) {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return expense;
        }


        public async Task<Expense> DeleteExpense(int expenseId) {
           var expense =  await GetExpense(expenseId);
            _context.Expenses.Remove(expense);
            await  _context.SaveChangesAsync();
            return expense;
        }

        public async Task<List<Expense>> GetAllExpenses(string userId) {
            var expenses = await  _context.Expenses.Where(e => e.UserId == userId).ToListAsync();
            return expenses;
        }


        public async  Task<Expense> GetExpense(int expenseId) {
            var expense = await _context.Expenses.SingleAsync(e => e.ExpenseId == expenseId);
            return expense;

        }

        public async Task<List<Expense>> GetAllExpenses(string userId, DateTime starTime, DateTime endTime) {

            var expenses = await _context.Expenses.Where(e =>
                (e.UserId == userId && e.ExpenseDate >= starTime && e.ExpenseDate <= endTime)).ToListAsync();

            return expenses;
        }



    }
}