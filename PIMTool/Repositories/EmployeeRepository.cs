using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Database;
using System.Threading;

namespace PIMTool.Repositories
{
    public  class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        private readonly PimContext _pimContext;
        private readonly DbSet<Employee> _set;

        public EmployeeRepository(PimContext pimContext) : base(pimContext)
        {
            _pimContext = pimContext;
            _set = pimContext.Set<Employee>();
        }

        public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            _set.Add(employee);
            await _pimContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRangeEmployeeAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken = default)
        {
            await _set.AddRangeAsync(employees);
            await _pimContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Employee?> DeleteEmployeeAsync(Employee[] employees, CancellationToken cancellationToken)
        {
            _set.RemoveRange(employees);
            await _pimContext.SaveChangesAsync(cancellationToken);
            return null;
        }

        public async Task<List<Employee?>> GetAllEmployeeAsync(CancellationToken cancellationToken = default)
        {
            return await Get().ToListAsync(cancellationToken);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _set.FirstOrDefaultAsync<Employee>(e => e.Id == id, cancellationToken);
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            _set.Update(employee);
            await _pimContext.SaveChangesAsync(cancellationToken);
            return employee;
        }
    }
}