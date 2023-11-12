using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;

namespace PIMTool.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeService(IRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            await _employeeRepo.AddAsync(employee, cancellationToken);
        }

        public async Task AddRangeEmployeeAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken = default)
        {
            await _employeeRepo.AddRangeAsync(employees, cancellationToken);
        }

        public async Task DeleteEmployees(Employee[] employees, CancellationToken cancellationToken)
        {
            await _employeeRepo.Delete(employees, cancellationToken);
        }

        public async Task<Employee?> GetEmployeeIdAsync(int id, CancellationToken cancellationToken = default)
        {
           return await _employeeRepo.GetIdAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Employee>> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            return await _employeeRepo.GetAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _employeeRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            await _employeeRepo.UpdateAsync(employee, cancellationToken);
        }
    }
}