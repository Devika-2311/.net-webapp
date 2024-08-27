using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AssignmentWebApi.Dto
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Joining is required")]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,10}$", ErrorMessage = "Password must be alphanumeric and between 8 to 10 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(1, 100000000, ErrorMessage = "Salary must be greater than 0 and less than 1 crore")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        public string DepartmentName {  get; set; }
    }
}
