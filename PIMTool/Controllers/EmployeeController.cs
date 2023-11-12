using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Employee>>> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetEmployeesAsync(cancellationToken);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetEmployeeIdAsync(id, cancellationToken);
            return employee;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee, CancellationToken cancellationToken)
        {
            await _employeeService.AddEmployeeAsync(employee, cancellationToken);
            return Ok();
        }

        [HttpPost("range")]
        public async Task<IActionResult> AddRangeEmployee(IEnumerable<Employee> employees, CancellationToken cancellationToken)
        {
            await _employeeService.AddRangeEmployeeAsync(employees, cancellationToken);
            return Ok();
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, CancellationToken cancellationToken)
        {
            var employee = _employeeService.GetEmployeeIdAsync(id, cancellationToken);
            if(employee == null)
            {
                return NotFound();
            }
            await _employeeService.UpdateEmployeeAsync(await employee, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployees(Employee[] employees, CancellationToken cancellationToken)
        {
            if(employees == null)
            {
                return NotFound();
            }
            await _employeeService.DeleteEmployees(employees, cancellationToken);
            return Ok();
        }

    }
}