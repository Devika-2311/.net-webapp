namespace AssignmentWebApi.Dto
{
    public class SalaryDto
    {
        public int SlipID { get; set; }

        public DateTime SlipDate { get; set; }
        public decimal Salary { get; set; }
        public int TotalLeaves { get; set; }
        public decimal NetSalary { get; set; }

        
    }
}
