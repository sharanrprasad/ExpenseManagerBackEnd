using Microsoft.EntityFrameworkCore;

namespace ExpenseManagerBackEnd.Models.DbModels {
    public class ExpenseManagerContext : DbContext {
        public ExpenseManagerContext(DbContextOptions<ExpenseManagerContext> options) : base(options) {
        }

        public DbSet<Budget> Budgets{ get; set; }
        public DbSet<Expense> Expenses{ get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories{ get; set; }
        public DbSet<User> Users{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("User").HasAlternateKey(u => u.Email);

            modelBuilder.Entity<Expense>().ToTable("Expense");

            modelBuilder.Entity<ExpenseCategory>().ToTable("ExpenseCategory").HasOne(ec => ec.ExpenseCategoryParent)
                .WithMany(ecp => ecp.ChildCategories).HasForeignKey(ec => ec.ParentId).OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Budget>().ToTable("Budget");
        }
    }
}