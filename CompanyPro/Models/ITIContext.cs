using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyPro.Models
{
    public class ITIContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        //public DbSet<ApplicationUser> Users { get; set; }  // already inherited
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        //public ITIContext():base() { }
        //public ITIContext(DbContextOptions options) :base() { }
        public ITIContext(DbContextOptions<ITIContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseLazyLoadingProxies()
        //        .UseSqlServer("Data Source=.;Initial Catalog=DotNet_ST_G4;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(new Department() { Did = 1, Name = "SD", Location = "Smart" });
            modelBuilder.Entity<Department>().HasData(new Department() { Did = 2, Name = "OS", Location = "Menofia" });

            modelBuilder.Entity<Employee>().HasData(new Employee() { EId = 1, Name = "Zeniab", Salary = 1000, Address = "Cairo", DepartmentId = 1});
            modelBuilder.Entity<Employee>().HasData(new Employee() { EId = 2, Name = "MoEbrahim", Salary = 1000, Address = "Menofia", DepartmentId = 2});
            modelBuilder.Entity<Employee>().HasData(new Employee() { EId = 3, Name = "Lena", Salary = 1100, Address = "Giza", DepartmentId = 1});
        }
    }
}


/*
 
 class Parent{}

 class Child: Parent{}

 
 
 */