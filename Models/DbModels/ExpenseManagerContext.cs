using Microsoft.EntityFrameworkCore;

namespace ExpenseManagerBackEnd.Models.DbModels
{
    public class ExpenseManagerContext : DbContext
    {
        public ExpenseManagerContext(DbContextOptions<ExpenseManagerContext> options) : base(options)
        {
            
        }
        
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Expense>().ToTable("Expense");
            modelBuilder.Entity<ExpenseCategory>().ToTable("ExpenseCategory");
            modelBuilder.Entity<Budget>().ToTable("Budget");
            
            
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Email);

        }
    }
}