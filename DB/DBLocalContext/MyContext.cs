using Microsoft.EntityFrameworkCore;
using DB.Entities;

namespace DB
{
    public class MyContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Database=SummerProject;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                   .HasIndex(u => u.Email)
                   .IsUnique();

            modelBuilder.Entity<User>()
                   .HasIndex(u => u.UserId)
                   .IsUnique();
        }
    }
}