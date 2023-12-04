using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Database;

namespace PIMTool.Services
{
    public class ProjectService : IProjectService
    {
        private readonly PimContext _pimContext;
        private readonly IRepository<Project> _projectRepo;

        public ProjectService(PimContext pimContext, IRepository<Project> projectRepo)
        {
            _pimContext = pimContext;
            _projectRepo = projectRepo;
        }

        public async Task<IReadOnlyCollection<Project>> GetProjectsAsyncWithSpec(ISpecification<Project> spec, CancellationToken cancellationToken)
        {
            return await _projectRepo.GetAsyncWithSpec(spec, cancellationToken);
        }

        public async Task<Project?> GetProjectWithSpec(ISpecification<Project> spec, CancellationToken cancellationToken)
        {
            return await _projectRepo.GetEntityWithSpec(spec, cancellationToken);
        }

        public async Task AddProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            await _projectRepo.AddAsync(project, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Employee>> GetEmployeesByProjectId(int id, CancellationToken cancellationToken)
        {
            var employees = await _pimContext.ProjectEmployees
                                    .Include(pe => pe.Employee)
                                    .Where(pe => pe.Project_Id == id)
                                    .Select(pe => pe.Employee)
                                    .ToListAsync(cancellationToken);
            return employees;
        }

        public async Task<int> CountProjectsAsync(ISpecification<Project> spec, CancellationToken cancellationToken = default)
        {
            return await _projectRepo.CountAsync(spec, cancellationToken);
        }

        public async Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            await _projectRepo.UpdateAsync(project, cancellationToken);
        }

        public void DeleteProjects(params Project[] projects)
        {
            _projectRepo.Delete(projects);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           await _projectRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task<Project?> FindByNumberAsync(int number)
        {
            return await _pimContext.Set<Project>().FirstOrDefaultAsync(x => x.Project_Number == number);
        }

        public async Task AddMembersAsync(ProjectEmployee[] projectEmployee)
        {
           await _pimContext.Set<ProjectEmployee>().AddRangeAsync(projectEmployee);
           await SaveChangesAsync(); 
        }

        public async Task<int> GetMaxProjectIdAsync()
        {
            return await _pimContext.Set<Project>().MaxAsync(p => p.Id);
        }
    }
}