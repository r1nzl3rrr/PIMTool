using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task AddRangeEmployeeAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken = default);
        Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Employee?>> GetAllEmployeeAsync(CancellationToken cancellationToken = default);
        Task<Employee?> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<Employee?> DeleteEmployeeAsync(Employee[] employees, CancellationToken cancellationToken = default);
    }
}
