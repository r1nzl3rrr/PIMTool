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


        void DeleteProjects(params Project[] projects);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);

        Task<Project?> FindByNumberAsync(int number);
    }
}