using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Database;

namespace PIMTool.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeService(IRepository<Employee> employeeRepo, PimContext pimContext)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            await _employeeRepo.AddAsync(employee, cancellationToken);
        }

        public async Task<int> CountEmployeesAsync(ISpecification<Employee> spec, CancellationToken cancellationToken = default)
        {
            return await _employeeRepo.CountAsync(spec, cancellationToken);
        }

        public async Task DeleteEmployee(Employee employee, CancellationToken cancellationToken)
        {
            await _employeeRepo.Delete(employee, cancellationToken);
        }

        public async Task<Employee?> GetEmployeeIdAsync(int id, CancellationToken cancellationToken = default)
        {
           return await _employeeRepo.GetIdAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Employee>> GetEmployeesAsyncWithSpec(ISpecification<Employee> spec, CancellationToken cancellationToken)
        {
            return await _employeeRepo.GetAsyncWithSpec(spec, cancellationToken);
        }

        public async Task UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            await _employeeRepo.UpdateAsync(employee, cancellationToken);
        }
    }
}