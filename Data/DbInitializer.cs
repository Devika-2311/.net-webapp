using AssignmentWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssignmentWebApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(EmployeeContext context)
        {
            context.Database.EnsureCreated();

           
            if (context.Departments.Any())
            {
                return; 
            }

           
            var departments = new List<Department>
            {
                new Department { DepartmentName = "HR" },
                new Department { DepartmentName = "IT" },
                new Department { DepartmentName = "Marketing" }
               
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee
                {
                    Name = "Tina",
                    DateOfJoining = DateTime.Parse("2020-01-01"),
                    Email = "tina@gmail.com",
                    Phone = "1234567890",
                    password = "password123",
                    Salary = 2500m,
                    role = "admin",
                    DepartmentId = departments[1].DepartmentId
                },
                new Employee
                {
                    Name = "Mary",
                    DateOfJoining = DateTime.Parse("2019-05-01"),
                    Email = "mary@gmail.com",
                    Phone = "0987654321",
                    password = "password123",
                    Salary = 3500m,
                    role = "employee",
                    DepartmentId = departments[0].DepartmentId
                },
                new Employee
                {
                    Name = "Alice",
                    DateOfJoining = DateTime.Parse("2018-03-15"),
                    Email = "alice@gmail.com",
                    Phone = "1122334455",
                    password = "alice123",
                    Salary = 4500m,
                    role = "employee",
                    DepartmentId = departments[2].DepartmentId 
                },
                new Employee
                {
                    Name = "Arjun",
                    DateOfJoining = DateTime.Parse("2021-02-20"),
                    Email = "arjun@gmail.com",
                    Phone = "6677889900",
                    password = "arjunpassword",
                    Salary = 3000m,
                    role = "employee",
                    DepartmentId = departments[1].DepartmentId 
                },
                new Employee
                {
                    Name = "Divya",
                    DateOfJoining = DateTime.Parse("2022-08-10"),
                    Email = "divya@gmail.com",
                    Phone = "2233445566",
                    password = "divya123",
                    Salary = 3200m,
                    role = "employee",
                    DepartmentId = departments[2].DepartmentId 
                }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
  // Seed Employee Leaves
            var employeeLeaves = new List<EmployeeLeave>
            {
                new EmployeeLeave
                {
                    EmployeeId = employees[0].EmployeeId,
                    StartDate = DateTime.Parse("2024-06-01"),
                    EndDate = DateTime.Parse("2024-06-10"),
                    TotalDays = 10
                },
                new EmployeeLeave
                {
                    EmployeeId = employees[1].EmployeeId,
                    StartDate = DateTime.Parse("2024-07-01"),
                    EndDate = DateTime.Parse("2024-07-05"),
                    TotalDays = 5
                },
                new EmployeeLeave
                {
                    EmployeeId = employees[2].EmployeeId,
                    StartDate = DateTime.Parse("2024-07-10"),
                    EndDate = DateTime.Parse("2024-07-15"),
                    TotalDays = 6
                },
                new EmployeeLeave
                {
                    EmployeeId = employees[3].EmployeeId,
                    StartDate = DateTime.Parse("2024-05-15"),
                    EndDate = DateTime.Parse("2024-05-20"),
                    TotalDays = 6
                },
                new EmployeeLeave
                {
                    EmployeeId = employees[4].EmployeeId,
                    StartDate = DateTime.Parse("2024-06-10"),
                    EndDate = DateTime.Parse("2024-06-12"),
                    TotalDays = 3
                },
                 new EmployeeLeave
                {
                    EmployeeId = employees[4].EmployeeId,
                    StartDate = DateTime.Parse("2024-08-10"),
                    EndDate = DateTime.Parse("2024-08-13"),
                    TotalDays = 4
                },
                 new EmployeeLeave
                {
                    EmployeeId = employees[2].EmployeeId,
                    StartDate = DateTime.Parse("2024-08-15"),
                    EndDate = DateTime.Parse("2024-07-16"),
                    TotalDays = 2
                }
                 ,
                new EmployeeLeave
                {
                    EmployeeId = employees[1].EmployeeId,
                    StartDate = DateTime.Parse("2024-08-05"),
                    EndDate = DateTime.Parse("2024-08-05"),
                    TotalDays = 1
                }
            };

            context.EmployeeLeaves.AddRange(employeeLeaves);
            context.SaveChanges();

          
            var salarySlips = new List<SalarySlip>
            {
                new SalarySlip
                {
                    EmployeeId = employees[0].EmployeeId,
                    SlipDate = DateTime.Parse("2024-06-30"),
                    Salary=employees[0].Salary,
                    NetSalary = 53000m,
                    TotalLeaves = 10
                },
                new SalarySlip
                {
                    EmployeeId = employees[1].EmployeeId,
                    SlipDate = DateTime.Parse("2024-07-31"),
                    Salary=employees[1].Salary,
                    NetSalary = 58000m,
                    TotalLeaves = 5
                },
                new SalarySlip
                {
                    EmployeeId = employees[2].EmployeeId,
                    SlipDate = DateTime.Parse("2024-07-31"),
                    Salary=employees[2].Salary,
                    NetSalary = 56000m,
                    TotalLeaves = 6
                },
                new SalarySlip
                {
                    EmployeeId = employees[3].EmployeeId,
                    SlipDate = DateTime.Parse("2024-05-31"),
                    Salary=employees[3].Salary,
                    NetSalary = 29000m,
                    TotalLeaves = 6
                },
                new SalarySlip
                {
                    EmployeeId = employees[4].EmployeeId,
                    SlipDate = DateTime.Parse("2024-06-30"),
                    Salary=employees[4].Salary,
                    NetSalary = 31000m,
                    TotalLeaves = 3
                }
            };

            context.SalarySlips.AddRange(salarySlips);
            context.SaveChanges();
        }
    }
}
