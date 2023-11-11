using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project> AddProjectAsync(Project project, CancellationToken cancellationToken = default);
        Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default);

        Task SaveProject(Project project, CancellationToken cancellationToken = default);

        Task DeleteProject(Project[] projects, CancellationToken cancellationToken = default);

        Task<List<Employee>> GetEmployeesByProjectId(int id, CancellationToken cancellationToken);

        Task<List<Project>> SearchProjects(string projectName, string customerName, string projectStatus, CancellationToken cancellationToken = default);

        Task<List<Project>> SearchProjectsAdvanced(string projectName, string customerName, string projectStatus, string groupLeaderVisa, CancellationToken cancellationToken = default);
    }
}