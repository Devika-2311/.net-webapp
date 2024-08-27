using AssignmentWebApi.Data;
using AssignmentWebApi.Dto;
using AssignmentWebApi.Interfaces;
using AssignmentWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : Controller
    {
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;
        private readonly EmployeeContext _context;

        [HttpPost("GenerateSalary")]
        public IActionResult GenerateSalarySlip()
        {
            _employee.GenerateSalarySlip();
            return Ok("Salary slips generated and sent successfully for all employees.");
        }
        public SalaryController(IEmployee employee, IMapper mapper, EmployeeContext context)
        {
            _employee = employee;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<SalarySlip>> GetAllSalarySlips()
        {
            var salarySlips = _context.SalarySlips
                                      .Include(s => s.Employee)
                                      .ToList();

            return Ok(salarySlips);

        }
        [HttpGet("GetSalary")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SalarySlip>))]
        public IActionResult GetAllSalarySlip()
        {
            var salarySlips = _mapper.Map<List<SalaryDto>>(_employee.GetSalarySlip());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(salarySlips);
        }

        [HttpGet("salary/{id}")]
        [ProducesResponseType(200, Type = typeof(SalaryDto))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult GetRecentSalarySlipByEmployeeId(int id)
        {
            if (!_employee.EmployeeExists(id))
                return NotFound();

            var salarySlip = _employee.GetRecentSalarySlipByEmployeeId(id);

            if (salarySlip == null)
            {
                return NoContent(); 
            }

            var salaryDto = _mapper.Map<SalaryDto>(salarySlip);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(salaryDto);
        }

    }
}
