namespace AssignmentWebApi.Dto
{
    public class EmployeeEdit
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public decimal Salary { get; set; }
        public string DepartmentName { get; set; }
    }
}
