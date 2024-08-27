using AssignmentWebApi.Data;
using AssignmentWebApi.Dto;
using AssignmentWebApi.Interfaces;
using AssignmentWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;
        private readonly EmployeeContext _context;
        public EmployeeController(IEmployee employee, IMapper mapper, EmployeeContext context)
        {
            _employee = employee;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid login request.");
            }

            var employee = _employee.Login(loginDto.Email, loginDto.password);

            if (employee == null)
            {
                return Unauthorized("Invalid email or password."); 
            }

            var employee1 = _mapper.Map<EmployeeDto>(_employee.GetEmployee(employee.EmployeeId));
            return Ok(employee1);
        }


        [HttpGet("employees")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employee = _mapper.Map<List<EmployeeDto>>(_employee.GetEmployees());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(employee);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int id)
        {
            if (!_employee.EmployeeExists(id))
                return NotFound();
            var employee = _mapper.Map<EmployeeDto>(_employee.GetEmployeeDto(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(employee);
        }

        [HttpGet("Edit/{id}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeeEdit(int id)
        {
            if (!_employee.EmployeeExists(id))
                return NotFound();
            var employee = _mapper.Map<EmployeeEdit>(_employee.GetEmployeeDto(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(employee);
        }

        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeecreate)
        {
            if (employeecreate == null)
                return BadRequest(ModelState);

            var employees = _employee.GetEmployees()
                .Where(c => c.Email.Trim().ToUpper() == employeecreate.Email.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (employees != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeMap = _mapper.Map<Employee>(employeecreate);

            employeeMap.Department = _employee.GetDepartmentByName(employeecreate.DepartmentName);

            if (!_employee.CreateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpGet("IsEmailUnique")]
        public IActionResult IsEmailUnique(string email, int? employeeId = null)
        {
            var employee = _context.Employees
                                   .Where(e => e.Email.ToUpper() == email.Trim().ToUpper())
                                   .FirstOrDefault();

            if (employee != null)
            {
               
                if (employeeId.HasValue && employee.EmployeeId == employeeId.Value)
                {
                    return Ok(true);
                }
                return Ok(false); 
            }

            return Ok(true); 
        }




        [HttpPut("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeEdit updatedEmployee)
        {
            if (updatedEmployee == null)
                return BadRequest(ModelState);

            if (employeeId != updatedEmployee.EmployeeId)
                return BadRequest(ModelState);

            if (!_employee.EmployeeExists(employeeId))
                return NotFound();

            var deptId = _employee.GetDepartmentIdByEmployeeId(employeeId);
            var department = _employee.GetDepartment(deptId);
            if (department == null)
            {
                ModelState.AddModelError("", "Invalid DepartmentId.");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();


            var ownerMap = _mapper.Map<Employee>(updatedEmployee);
            ownerMap.Department = _employee.GetDepartment(deptId);

            if (!_employee.UpdateEmployee(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating employee");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int employeeId)
        {
            if (!_employee.EmployeeExists(employeeId))
            {
                return NotFound();
            }

            var empToDelete = _employee.GetEmployee(employeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employee.DeleteEmployee(empToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting employee");
            }

            return NoContent();
        }
    }
}
