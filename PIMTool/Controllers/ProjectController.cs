using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Database;
using PIMTool.Dtos;
using PIMTool.Errors;
using PIMTool.Helpers;
using PIMTool.Services;

namespace PIMTool.Controllers
{
    public class ProjectController : BaseApiController
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
        public async Task<ActionResult<IReadOnlyCollection<ProjectDto>>> GetProjectsAsync([FromQuery]ProjectSpecParams projectParams, CancellationToken cancellationToken)
        {
            var spec = new ProjectSpecification(projectParams);

            var countSpec = new ProjectWithFilterForCountSpecification(projectParams);

            var totalItems = await _projectService.CountProjectsAsync(countSpec);

            var projects = await _projectService.GetProjectsAsyncWithSpec(spec, cancellationToken);

            var data = _mapper.Map<IReadOnlyCollection<Project>, IReadOnlyCollection<ProjectDto>>(projects);

            return Ok(new Pagination<ProjectDto>(projectParams.PageIndex, projectParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto?>> GetProjectIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var spec = new ProjectSpecification(id);
            var project = await _projectService.GetProjectWithSpec(spec, cancellationToken);
            if (project == null)
            {
                return NotFound(new ApiResponse(404));
            }
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

            var spec = new ProjectSpecification(id);
            var project = await _projectService.GetProjectWithSpec(spec, cancellationToken);
            if (project == null)
            {
                return NotFound(new ApiResponse(404));
            }
            await _projectService.UpdateProjectAsync(project, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProjects(int id, CancellationToken cancellationToken)
        {
            var project = GetProjectIdAsync(id, cancellationToken);
            if (project == null)
            {
                return NotFound(new ApiResponse(404));
            }
            await _projectService.DeleteProjects(id, cancellationToken);
            return Ok();
        }

        [HttpGet("get-employees/{id}")]
        public async Task<ActionResult<IReadOnlyCollection<EmployeeDto>>> GetEmployees(int id, CancellationToken cancellationToken)
        {
            var employees = await _projectService.GetEmployeesByProjectId(id, cancellationToken);
            return Ok(_mapper.Map<IReadOnlyCollection<Employee>, IReadOnlyCollection<EmployeeDto>>(employees));
        }

        
    }
}