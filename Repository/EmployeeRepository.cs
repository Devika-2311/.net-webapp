using AssignmentWebApi.Data;
using AssignmentWebApi.Models;
using AssignmentWebApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Net.Mail;
using System.Net;
using AssignmentWebApi.Dto;
using AutoMapper;

namespace AssignmentWebApi.Repository
{
    public class EmployeeRepository:IEmployee
    {
        private readonly EmployeeContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(EmployeeContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<Employee> GetEmployees()
        {

            return _context.Employees
                .Include(e => e.Department) 
                .OrderBy(p => p.EmployeeId)
                .ToList();
        }
        public EmployeeDto GetEmployeeDto(int id)
        {
            var employee = _context.Employees
                           .Include(e => e.Department)
                           .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return null;

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            employeeDto.DepartmentName = employee.Department.DepartmentName;

            return employeeDto;
        }
        public Employee GetEmployee(int id)
        {
            return _context.Employees.Where(p => p.EmployeeId == id).FirstOrDefault();
        }
        public Employee GetEmployeeEdit(int id)
        {
            return _context.Employees.Where(p => p.EmployeeId == id).FirstOrDefault();
        }
        public bool CreateEmployee(Employee employee)
        {
            _context.Add(employee);
            return Save();
        }
        public bool Save()
        {
            var saved=_context.SaveChanges();
            return saved>0?true:false;
        }
        public bool EmployeeExists(int id)
        {
            return _context.Employees.Any(p => p.EmployeeId == id);
        }
        public bool UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            return Save();
        }
        public bool DeleteEmployee(Employee employee)
        {
           _context.Remove(employee);
            return Save();
        }
        public Department GetDepartmentByName(string departmentName)
        {
            return _context.Departments
          .Where(d => d.DepartmentName.ToLower() == departmentName.ToLower())
          .FirstOrDefault();
        }
        public Department GetDepartment(int id)
        {
            return _context.Departments.Where(p => p.DepartmentId == id).FirstOrDefault();
        }


        public int GetDepartmentIdByEmployeeId(int employeeId)
        {
            
            var employee = _context.Employees
                                   .Where(e => e.EmployeeId == employeeId)
                                   .Select(e => e.DepartmentId)
                                   .FirstOrDefault();

            
            return employee;
        }
        public ICollection<SalarySlip> GetSalarySlip()
        {
            return _context.SalarySlips.OrderBy(p => p.SlipID).ToList();
        }
        public SalarySlip GetRecentSalarySlipByEmployeeId(int employeeId)
        {

            return _context.SalarySlips
                           .Where(s => s.EmployeeId == employeeId)
                           .OrderByDescending(s => s.SlipDate)
                           .FirstOrDefault();
        }
        public Employee Login(string email, string password)
        {
            return _context.Employees
                .FirstOrDefault(e => e.Email == email && e.password == password);
        }

        public void GenerateSalarySlip()
        {
            var employees = _context.Employees.Include(e => e.EmployeeLeaves).ToList();
            var allowedLeaves = 12;
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var extraLeaves=0;

            foreach (var employee in employees)
            {

                var totalLeavesTaken = employee.EmployeeLeaves
                                       .Where(leave => leave.StartDate.Year == currentYear && leave.StartDate.Month != currentMonth)
                                       .Sum(leave => leave.TotalDays);

               
                var leavesTakenThisMonth = employee.EmployeeLeaves
                                                   .Where(leave => leave.StartDate.Month == currentMonth && leave.StartDate.Year == currentYear)
                                                   .Sum(leave => leave.TotalDays);

               
               // var totalLeavesWithThisMonth = totalLeavesTaken + leavesTakenThisMonth;

                decimal leaveDeduction = 0;
                var dailySalary = employee.Salary / 30; 

                if (totalLeavesTaken > allowedLeaves)
                {
                    var extraLeave = leavesTakenThisMonth;
                    leaveDeduction = extraLeave * dailySalary;
                    extraLeaves = extraLeave;
                }

                var netSalary = employee.Salary - leaveDeduction;

                var salarySlip = new SalarySlip
                {
                    EmployeeId = employee.EmployeeId,
                    SlipDate = DateTime.Now,
                    Salary=employee.Salary,
                    TotalLeaves = extraLeaves,
                    NetSalary = netSalary,
                   
                };

                _context.SalarySlips.Add(salarySlip);

                
                SendSalarySlipEmail(employee.Email, salarySlip, employee.Name);
            }

            _context.SaveChanges();
        }

        private void SendSalarySlipEmail(string toEmail, SalarySlip salarySlip, string employeeName)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("ashokmanu89@gmail.com", "idwc lmdc iold phdp"),
                    EnableSsl = true,
                };
                var fromAddress = new MailAddress("ashokmanu89@gmail.com", "Salary");
                var mailMessage = new MailMessage
                {
                    From = fromAddress,
                    Subject = "Your Salary Slip for " + salarySlip.SlipDate.ToString("MMMM yyyy"),
                    Body = $"Dear {employeeName},\n\n" +
                           $"Please find your salary slip for {salarySlip.SlipDate.ToString("MMMM yyyy")} below:\n\n" +
                           $"Net Salary: {salarySlip.NetSalary:C}\n" +
                           $"Total Leaves Taken: {salarySlip.TotalLeaves}\n\n" +
                           "Thank you,\n" +
                           "HR Department",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(toEmail);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error sending email to {toEmail}: {ex.Message}");
            }
        }

    }
}
