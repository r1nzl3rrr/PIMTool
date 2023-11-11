using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            await _employeeRepository.AddEmployeeAsync(employee, cancellationToken);
        }

        public async Task AddRangeEmployeeAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken = default)
        {
            await _employeeRepository.AddRangeEmployeeAsync(employees, cancellationToken);
        }

        public async Task<Employee?> DeleteEmployeeAsync(Employee[] employees, CancellationToken cancellationToken = default)
        {
            return await _employeeRepository.DeleteEmployeeAsync(employees, cancellationToken);
        }

        public async Task<List<Employee?>> GetAllEmployeeAsync(CancellationToken cancellationToken = default)
        {
            return await _employeeRepository.GetAllEmployeeAsync(cancellationToken);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id, cancellationToken);
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            return await _employeeRepository.UpdateEmployeeAsync(employee, cancellationToken);
        }
    }
}