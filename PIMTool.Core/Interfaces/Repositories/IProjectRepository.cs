using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<Employee>> GetEmployeesByProjectId(int id, CancellationToken cancellationToken);

        Task SaveProject(Project project, CancellationToken cancellationToken = default);

        Task DeleteProject(Project[] projects, CancellationToken cancellationToken = default);

        Task<List<Project>> SearchProjects(string projectName, string customerName, string projectStatus, CancellationToken cancellationToken = default);

        Task<List<Project>> SearchProjectsAdvanced(string projectName, string customerName, string projectStatus, string groupLeaderVisa, CancellationToken cancellationToken = default);
    }
}
