using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectReferencesDemo.Services.Models;

namespace ProjectReferencesDemo.Services.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;" +
                "Database=aspnet-ProjectReferencesDemo.Web-DD62A3E4-4593-4C6A-8194-26752105AB04;" +
                "Trusted_Connection=True;MultipleActiveResultSets=true"
            );

            base.OnConfiguring(optionsBuilder);
        }
    }
}