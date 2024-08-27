using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AssignmentWebApi.Models
{
    public class SalarySlip
    {
        [Key]
        public int SlipID { get; set; }

        public DateTime SlipDate { get; set; }
        public decimal Salary {  get; set; }
        public int TotalLeaves { get; set; }
        public decimal NetSalary { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
