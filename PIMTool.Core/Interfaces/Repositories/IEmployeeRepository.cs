using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task AddRangeEmployeeAsync(IEnumerable<Employee> employee, CancellationToken cancellationToken = default);
        Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Employee?>> GetAllEmployeeAsync(CancellationToken cancellationToken = default);
        Task<Employee?> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<Employee?> DeleteEmployeeAsync(Employee[] employees, CancellationToken cancellationToken = default);
    }
}
