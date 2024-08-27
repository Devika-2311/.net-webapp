using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentWebApi.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name {  get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Email {  get; set; }
        public string password {  get; set; }
        public string Phone {  get; set; }
        public string role { get;set; }
        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public ICollection<EmployeeLeave> EmployeeLeaves { get; set; }
        
        public ICollection<SalarySlip> SalarySlip { get; set; }
    }
}
