using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Database;
using PIMTool.Dtos;
using PIMTool.Services;

namespace PIMTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService,
            IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<ProjectDto>>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            var spec = new ProjectsWithGroupsSpecification();
            var projects = await _projectService.GetProjectsAsyncWithSpec(spec, cancellationToken);
            return Ok(_mapper.Map<IReadOnlyCollection<Project>, IReadOnlyCollection<ProjectDto>>(projects));
        }

        [HttpGet("{id}")]
        public async Task<ProjectDto?> GetProjectIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var spec = new ProjectsWithGroupsSpecification(id);
            var project = await _projectService.GetProjectWithSpec(spec, cancellationToken);
            return _mapper.Map<Project, ProjectDto>(project);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(Project project, CancellationToken cancellationToken)
        {
            await _projectService.AddProjectAsync(project, cancellationToken);
            return Ok();
        }

        [HttpPost("range")]
        public async Task<IActionResult> AddRangeProject(IEnumerable<Project> projects, CancellationToken cancellationToken)
        {
            await _projectService.AddRangeProjectAsync(projects, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, CancellationToken cancellationToken)
        {

            var spec = new ProjectsWithGroupsSpecification(id);
            var project = await _projectService.GetProjectWithSpec(spec, cancellationToken);
            if (project == null)
            {
                return NotFound();
            }
            await _projectService.UpdateProjectAsync(project, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProjects(Project[] projects, CancellationToken cancellationToken)
        {
            if (projects == null)
            {
                return NotFound();
            }
            await _projectService.DeleteProjects(projects, cancellationToken);
            return Ok();
        }

        [HttpGet("get-employees/{id}")]
        public async Task<IReadOnlyCollection<Employee>> GetEmployees(int id, CancellationToken cancellationToken)
        {
            return await _projectService.GetEmployeesByProjectId(id, cancellationToken);
        }

        [HttpGet("find-projects/{projectName}/{customerName}/{projectStatus}")]
        public async Task<IReadOnlyCollection<ProjectDto>> FindProjects(string projectName, string customerName, string projectStatus, CancellationToken cancellationToken = default)
        {
            var spec = new ProjectsWithGroupsSpecification(projectName, customerName, projectStatus);
            var projects = await _projectService.GetProjectsAsyncWithSpec(spec, cancellationToken);
            return _mapper.Map<IReadOnlyCollection<Project>, IReadOnlyCollection<ProjectDto>>(projects);
        }

        [HttpGet("find-projects-advanced/{projectName}/{customerName}/{projectStatus}/{groupLeaderVisa}")]
        public async Task<IReadOnlyCollection<ProjectDto>> FindProjectsAdvanced(string projectName, string customerName, string projectStatus, string groupLeaderVisa, CancellationToken cancellationToken = default)
        {
            var spec = new ProjectsWithGroupsSpecification(projectName, customerName, projectStatus, groupLeaderVisa);
            var projects = await _projectService.GetProjectsAsyncWithSpec(spec, cancellationToken);
            return _mapper.Map<IReadOnlyCollection<Project>, IReadOnlyCollection<ProjectDto>>(projects);
        }


    }
}