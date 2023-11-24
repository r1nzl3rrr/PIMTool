using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Specifications;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IReadOnlyCollection<Employee>> GetEmployeesAsyncWithSpec(ISpecification<Employee> spec, CancellationToken cancellationToken);

        Task<Employee?> GetEmployeeIdAsync(int id, CancellationToken cancellationToken = default);

        Task<int> CountEmployeesAsync(ISpecification<Employee> spec, CancellationToken cancellationToken = default);

        Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);

        Task AddRangeEmployeeAsync(IList<Employee> employees);

        Task DeleteEmployee(Employee employee, CancellationToken cancellationToken);
        Task UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken);
    }
}
