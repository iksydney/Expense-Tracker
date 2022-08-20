using Microsoft.EntityFrameworkCore;

namespace expenseTracker.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<expenseTracker.Transaction> Transactions{ get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
