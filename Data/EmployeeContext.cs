using AssignmentWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentWebApi.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }

        public DbSet<SalarySlip> SalarySlips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);



            
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeLeaves)
                .WithOne(el => el.Employee)
                .HasForeignKey(el => el.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SalarySlip)
                .WithOne(ss => ss.Employee)
                .HasForeignKey(ss => ss.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SalarySlip>()
        .Property(s => s.NetSalary)
        .HasColumnType("decimal(18, 2)");

        }

    }
}
