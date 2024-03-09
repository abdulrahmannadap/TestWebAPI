using Microsoft.EntityFrameworkCore;
using TestWebAPI.Model;
using TestWebAPI.Model.DTO;

namespace TestWebAPI.Data
{
    public class ApplicationDbContext :DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDto> EmployeeDtos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id=1,Name="Test1",Salary=1000,UAN="ABCD1",CareatedDate=DateTime.Now},
                new Employee { Id=2,Name="Test2",Salary=2000,UAN="ABCD2",CareatedDate=DateTime.Now},
                new Employee { Id=3,Name="Test3",Salary=3000,UAN="ABCD3",CareatedDate=DateTime.Now},
                new Employee { Id=4,Name="Test4",Salary=4000,UAN="ABCD4",CareatedDate=DateTime.Now},
                new Employee { Id=5,Name="Test5",Salary=5000,UAN="ABCD5",CareatedDate=DateTime.Now}
                );
        }

    }
}
