using Microsoft.EntityFrameworkCore;
using DB.Entities;

namespace DB
{
    public class MyContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            Database.Migrate();
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