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
        Task<int> CountProjectsAsync(ISpecification<Project> spec, CancellationToken cancellationToken = default);

        Task AddProjectAsync(Project project, CancellationToken cancellationToken = default);

        Task DeleteProject(Project project, CancellationToken cancellationToken);

        Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);
    }
}