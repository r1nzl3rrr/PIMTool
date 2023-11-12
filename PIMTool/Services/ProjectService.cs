using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Database;
using PIMTool.Dtos;
using PIMTool.Repositories;
using PIMTool.SpecificationEvaluator;

namespace PIMTool.Services
{
    public class ProjectService : IProjectService
    {
        private readonly PimContext _pimContext;
        private readonly IRepository<Project> _projectRepo;
        private readonly IRepository<ProjectEmployee> _projectEmployeeRepo;

        public ProjectService(PimContext pimContext, IRepository<Project> projectRepo, IRepository<ProjectEmployee> projectEmployeeRepo, IMapper mapper)
        {
            _pimContext = pimContext;
            _projectRepo = projectRepo;
            _projectEmployeeRepo = projectEmployeeRepo;
        }

        public async Task<IReadOnlyCollection<Project>> GetProjectsAsyncWithSpec(ISpecification<Project> spec, CancellationToken cancellationToken)
        {
            return await _projectRepo.GetAsyncWithSpec(spec, cancellationToken);
        }

        public async Task<Project?> GetProjectWithSpec(ISpecification<Project> spec, CancellationToken cancellationToken)
        {
            return await _projectRepo.GetEntityWithSpec(spec, cancellationToken);
        }

        public async Task AddRangeProjectAsync(IEnumerable<Project> projects, CancellationToken cancellationToken = default)
        {
            await _projectRepo.AddRangeAsync(projects, cancellationToken);
            await SaveChangesAsync();
        }

        public async Task AddProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            await _projectRepo.AddAsync(project, cancellationToken);
            await SaveChangesAsync();
        }

        public async Task DeleteProjects(Project[] projects, CancellationToken cancellationToken)
        {
            await _projectRepo.Delete(projects, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _projectRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Employee>> GetEmployeesByProjectId(int id, CancellationToken cancellationToken)
        {
            var spec = new ProjectEmployeeSpecification(id);
            var employees = await _projectEmployeeRepo.GetAsyncWithSpec(spec, cancellationToken);
            return employees.Select(pe => pe.Employee).ToList();
        }

        public async Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            await _projectRepo.UpdateAsync(project, cancellationToken);
        }
    }
}