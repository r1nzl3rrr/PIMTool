using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Specifications;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<IReadOnlyCollection<Project>> GetProjectsAsyncWithSpec(ISpecification<Project> spec, CancellationToken cancellationToken);

        Task<Project?> GetProjectWithSpec(ISpecification<Project> spec, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<Employee>> GetEmployeesByProjectId(int id, CancellationToken cancellationToken);

        Task AddRangeProjectAsync(IEnumerable<Project> projects, CancellationToken cancellationToken = default);

        Task AddProjectAsync(Project project, CancellationToken cancellationToken = default);

        Task DeleteProjects(Project[] projects, CancellationToken cancellationToken);

        Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}