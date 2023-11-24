﻿using AutoMapper;
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

        public async Task AddRangeProjectAsync(IList<Project> projects)
        {
            await _projectRepo.AddRangeAsync(projects);
        }
        public async Task DeleteProject(Project project, CancellationToken cancellationToken)
        {
            await _projectRepo.Delete(project, cancellationToken);
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

        
    }
}