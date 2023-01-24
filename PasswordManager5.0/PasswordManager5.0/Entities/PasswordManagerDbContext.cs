using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager5._0.Entities
{
    public class PasswordManagerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }

        private string connectionString = "Server=tcp:zaliczenie-szczepaniak-haenel.database.windows.net,1433;Initial Catalog=Zaliczenie;Persist Security Info=False;User ID=M4ster;Password=ZAQ!2wsx;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //to extend
            modelBuilder.Entity<User>()
                .Property(r => r.userId)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }



    }
}
