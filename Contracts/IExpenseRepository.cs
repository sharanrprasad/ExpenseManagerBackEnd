using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Models.DbModels;

namespace ExpenseManagerBackEnd.Contracts {
    public interface IExpenseRepository {
        
        Task<Expense> AddExpense(Expense expense);
        Task<Expense> DeleteExpense(int expenseId);
        Task<List<Expense>> GetAllExpenses(string userId);
        Task<List<Expense>> GetAllExpenses(string userId, DateTime starTime, DateTime endTime);
        Task<Expense> GetExpense(int expenseId);
    }
}