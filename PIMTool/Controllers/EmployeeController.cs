using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Dtos;

namespace PIMTool.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<EmployeeDto>>> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetEmployeesAsync(cancellationToken);
            return Ok(_mapper.Map<IReadOnlyCollection<Employee>, IReadOnlyCollection<EmployeeDto>>(employees));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetEmployeeIdAsync(id, cancellationToken);
            return _mapper.Map<Employee, EmployeeDto>(employee);
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
        public async Task<IActionResult> DeleteEmployees(int id, CancellationToken cancellationToken)
        {
            var employee = _employeeService.GetEmployeeIdAsync(id, cancellationToken);
            if(employee == null)
            {
                return NotFound();
            }
            await _employeeService.DeleteEmployees(id, cancellationToken);
            return Ok();
        }

    }
}