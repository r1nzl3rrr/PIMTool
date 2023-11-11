using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> AddProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
           await _projectRepository.AddAsync(project, cancellationToken);
            return project;
        }

        public async Task DeleteProject(Project[] projects, CancellationToken cancellationToken = default)
        {
            await _projectRepository.DeleteProject(projects);
            await _projectRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _projectRepository.GetAsync(id, cancellationToken);
            return entity;
        }

        public async Task<List<Employee>> GetEmployeesByProjectId(int id, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetEmployeesByProjectId(id, cancellationToken);
        }

        public async Task SaveProject(Project project, CancellationToken cancellationToken = default)
        {
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task<List<Project>> SearchProjects(string projectName, string customerName, string projectStatus, CancellationToken cancellationToken = default)
        {
            return await _projectRepository.SearchProjects(projectName, customerName, projectStatus, cancellationToken); 

        }

        public async Task<List<Project>> SearchProjectsAdvanced(string projectName, string customerName, string projectStatus, string groupLeaderVisa, CancellationToken cancellationToken = default)
        {
            return await _projectRepository.SearchProjectsAdvanced(projectName, customerName, projectStatus, groupLeaderVisa, cancellationToken);
        }
    }
}