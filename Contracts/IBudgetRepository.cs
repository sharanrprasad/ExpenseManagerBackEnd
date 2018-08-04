using System;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.Models.DbModels;

namespace ExpenseManagerBackEnd.Contracts {
    public interface IBudgetRepository {
        Task<Budget> AddBudget(Budget budget);
        Task<Budget> GetCurrentBudget(string userId);
        Task<Budget> DeleteBudget(int budgetId);
        Task<Budget> UpdateBudget(Budget budget);
        Task<bool> Exists(int id);
        Task<bool> Exists(string userId, DateTime startTime, DateTime endTime);
    }
}