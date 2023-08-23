using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationMVC.Models
{
    public static class ModelBuilderExtensions
    {
        public static void seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 5,
                    Name = "Dima",
                    Email = "dima_shaar@yahoo.com",
                    Department = Dept.Payroll
                }
            );
        }
    }
}
