using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIMTool.AddingAndUpdatingDtos;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Dtos;
using PIMTool.Errors;
using PIMTool.Helpers;

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
        public async Task<ActionResult<Pagination<EmployeeDto>>> GetEmployeesAsync([FromQuery] EmployeeSpecParams employeeParams, 
            CancellationToken cancellationToken)
        {
            var spec = new EmployeeSpecification(employeeParams);

            var countSpec = new EmployeeWithFilterForCountSpecification(employeeParams);

            var totalItems = await _employeeService.CountEmployeesAsync(countSpec);

            var employees = await _employeeService.GetEmployeesAsyncWithSpec(spec, cancellationToken);

            var data = _mapper.Map<IReadOnlyCollection<Employee>, IReadOnlyCollection<EmployeeDto>>(employees);

            return Ok(new Pagination<EmployeeDto>(employeeParams.PageIndex, employeeParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetEmployeeIdAsync(id, cancellationToken);
            if(employee == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<Employee, EmployeeDto>(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEmployee([FromBody] AddingAndUpdatingEmployeeDto employee, CancellationToken cancellationToken)
        {
            Employee newEmployee = new()
            {
                First_Name = employee.First_Name,
                Last_Name = employee.Last_Name,
                Birth_Date = employee.Birth_Date,
                Visa = employee.Visa
            };

            await _employeeService.AddEmployeeAsync(newEmployee, cancellationToken);
            return Ok(new ApiResponse(200));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] AddingAndUpdatingEmployeeDto employee, CancellationToken cancellationToken)
        {
            var updatingEmployee = await _employeeService.GetEmployeeIdAsync(id, cancellationToken);

            if (updatingEmployee == null)
            {
                return NotFound(new ApiResponse(404));
            }
            updatingEmployee.Birth_Date = employee.Birth_Date;
            updatingEmployee.First_Name = employee.First_Name;  
            updatingEmployee.Last_Name = employee.Last_Name;
            updatingEmployee.Visa = employee.Visa;

            await _employeeService.UpdateEmployeeAsync(updatingEmployee, cancellationToken);
            return Ok(new ApiResponse(200));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetEmployeeIdAsync(id, cancellationToken);
            if (employee == null)
            {
                return NotFound(new ApiResponse(404));
            }
            await _employeeService.DeleteEmployee(employee, cancellationToken);
            return Ok(new ApiResponse(200));
        }

    }
}