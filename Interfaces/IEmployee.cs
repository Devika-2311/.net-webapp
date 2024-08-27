using AssignmentWebApi.Models;
using AssignmentWebApi.Dto;

namespace AssignmentWebApi.Interfaces
{
    public interface IEmployee
    {
        ICollection<Employee> GetEmployees();
        EmployeeDto GetEmployeeDto(int id);
        Employee GetEmployee(int id);
        Employee GetEmployeeEdit(int id);
        bool CreateEmployee(Employee employee);

        bool Save();

        bool EmployeeExists(int id);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);

        Department GetDepartmentByName(string name);

        Department GetDepartment(int id);
        int GetDepartmentIdByEmployeeId(int id);

        ICollection<SalarySlip> GetSalarySlip();
        SalarySlip GetRecentSalarySlipByEmployeeId(int employeeId);

        void GenerateSalarySlip();
        Employee Login(string email, string password);
    }
}
