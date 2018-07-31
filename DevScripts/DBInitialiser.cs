using System;
using System.Linq;
using ExpenseManagerBackEnd.Models.DbModels;

namespace ExpenseManagerBackEnd.DevScripts {
    
    public class DBInitialiser {
        
        public static void Initialise(ExpenseManagerContext context) {
            if (context.Users.Any()) {
                return;
            }

            var users = new User[] {
                new User() {
                    Email = "sharanrprasad@hotmail.com",
                    Name = "Sharan",
                    Password = "pass@123",
                    Phone = "+64275932755",
                    UserId = "123456"
                },
            };

            foreach (var user in users) {
                context.Users.Add(user);
            }

            context.SaveChanges();

            var expenseCategories = new ExpenseCategory[] {
                new ExpenseCategory() {ExpenseCategoryId = "412", Name = "Food"}
            };

            foreach (var category in expenseCategories) {
                context.ExpenseCategories.Add(category);
            }

            context.SaveChanges();

            var expenses = new Expense[] {
                new Expense() {
                    ExpenseCategoryId = "412",
                    ExpenseDate = DateTime.Now,
                    PaymentMethod = PaymentMethod.Cash,
                    Price = 20,
                    UserId = "123456"
                }
            };

            foreach (var expense in expenses) {
                context.Expenses.Add(expense);
            }

            context.SaveChanges();
        }

    }
}