using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApplicationMVC.Models
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions options)
            :base(options)    
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.seed();
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 4,
                    Name = "Rania",
                    Email = "Rania@yahoo.com",
                    Department = Dept.HR
                }
            );
        }
    }
}
