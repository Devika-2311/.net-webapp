using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentWebApi.Models
{
    public class EmployeeLeave
    {
        public int EmployeeLeaveId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get; set; }
        public string Reason {  get; set; }
        public int EmployeeId { get; set; }  

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
