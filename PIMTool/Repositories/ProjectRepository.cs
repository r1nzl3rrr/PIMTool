using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Database;
using PIMTool.Repositories;
using System.Linq.Dynamic.Core;

namespace PIMTool.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly PimContext _pimContext;
        private readonly DbSet<Project> _set;
        public ProjectRepository(PimContext pimContext) : base(pimContext)
        {
            _pimContext = pimContext;
            _set = pimContext.Set<Project>();
        }
        public async Task DeleteProject(Project[] projects, CancellationToken cancellationToken = default)
        {
            _set.RemoveRange(projects);
            await _pimContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Employee>> GetEmployeesByProjectId(int id, CancellationToken cancellationToken)
        {
            var employees = await _pimContext.ProjectEmployees
                                    .Include(pe => pe.Employee)
                                    .Where(pe => pe.Project_Id == id)
                                    .Select(pe => pe.Employee)
                                    .ToListAsync(cancellationToken);
            return employees;
        }

        public async Task SaveProject(Project project, CancellationToken cancellationToken = default)
        {
            await _set.AddAsync(project, cancellationToken);
            await _pimContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Project>> SearchProjects(string projectName, string customerName, string projectStatus, CancellationToken cancellationToken = default)
        {
            return await Get()
                    .Where(p => p.Name.Contains(projectName))
                    .Where(p => p.Customer.Contains(customerName))
                    .Where(p => p.Status == projectStatus)
                    .ToListAsync();
        }

        public async Task<List<Project>> SearchProjectsAdvanced(string projectName, string customerName, string projectStatus, string groupLeaderVisa, CancellationToken cancellationToken = default)
        {
            return await Get()
                        .Include(p => p.Group.Leader)
                        .Where(p => p.Name.Contains(projectName))
                        .Where(p => p.Customer.Contains(customerName))
                        .Where(p => p.Group.Leader.Visa == groupLeaderVisa)
                        .ToListAsync();
        }
    }
}
