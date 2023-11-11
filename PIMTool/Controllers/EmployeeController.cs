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

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if(employee == null)
            {
                return NotFound();
            }
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

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllEmployeeAsync(cancellationToken);
            return employees;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, CancellationToken cancellationToken)
        {
            var employee = _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(await employee, cancellationToken);
            if(updatedEmployee == null) { return NotFound(); }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployees(Employee[] employees, CancellationToken cancellationToken)
        {
            var deletedEmployee = await _employeeService.DeleteEmployeeAsync(employees, cancellationToken);
            if(deletedEmployee == null) { return NotFound();}
            return Ok();
        }

    }
}