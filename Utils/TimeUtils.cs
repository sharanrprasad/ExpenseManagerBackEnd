using System;
using ExpenseManagerBackEnd.Models.DbModels;

namespace ExpenseManagerBackEnd.Utils {
    public class TimeUtils {
        
        public static  Budget GetBudgetUtc(Budget budget) {
            budget.FromDate = TimeZoneInfo.ConvertTimeToUtc(budget.FromDate);
            budget.ToDate = TimeZoneInfo.ConvertTimeToUtc(budget.ToDate);
            return budget;
        }


        public static Expense GetExpenseUtc(Expense expense) {
            expense.ExpenseDate = TimeZoneInfo.ConvertTimeToUtc(expense.ExpenseDate);
            return expense;
        }
    }
}