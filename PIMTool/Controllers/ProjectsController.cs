using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using PIMTool.AddingAndUpdatingDtos;
using PIMTool.Config;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Dtos;
using PIMTool.Errors;
using PIMTool.Helpers;

namespace PIMTool.Controllers
{
    public class ProjectsController : BaseApiController
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService,
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

        [HttpGet("numberexists")]
        public async Task<ActionResult<bool>> CheckProjectNumberExistsAsync([FromQuery] int number){
            return await _projectService.FindByNumberAsync(number) != null;
        }

        [HttpPost("addmembers")]
        public async Task<ActionResult> AddMembers(int[] memIds){
            if(memIds == null || !memIds.Any()){
                return Ok(new ApiResponse(200));
            }
            int newProjectId = await _projectService.GetMaxProjectIdAsync();
            
            ProjectEmployee[] projectEmployees = memIds.Select(memId => new ProjectEmployee
            {
                Project_Id = newProjectId,
                Employee_Id = memId

            }).ToArray();

            await _projectService.AddMembersAsync(projectEmployees);

            return Ok(new ApiResponse(200));
        }

        [HttpPost("updatemembers/{id}")]
        public async Task<ActionResult> UpdateMembers(int id, int[] memIds){
            await _projectService.RemoveMemberAsync(id);

            if(memIds == null || !memIds.Any()){
               return Ok(new ApiResponse(200));
            }
            
            ProjectEmployee[] projectEmployees = memIds.Select(memId => new ProjectEmployee
            {
                Project_Id = id,
                Employee_Id = memId
                
            }).ToArray();

            await _projectService.AddMembersAsync(projectEmployees);

            return Ok(new ApiResponse(200));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProject(AddingAndUpdatingProjectDto project, CancellationToken cancellationToken)
        {
            if(project.Project_Number >= 10000){
                return new BadRequestObjectResult(
                    new ApiValidationErrorResponse{Errors = new[]
                        {"Project number must be less than 4 digits"}
                    }
                );
            }

            if(CheckProjectNumberExistsAsync(project.Project_Number).Result.Value) {
                return new BadRequestObjectResult(
                    new ApiValidationErrorResponse{Errors = new[]
                        {"The project number already existed. Please select a different project number"}
                    }
                );
            }
            
            Project newProject = new()
            {
                Group_Id = project.Group_Id,
                Project_Number = project.Project_Number,
                Name = project.Name,
                Customer = project.Customer,
                Status = project.Status,
                Start_Date = project.Start_Date,
                End_Date = project.End_Date,
            };

            await _projectService.AddProjectAsync(newProject, cancellationToken);
            return Ok(new ApiResponse(200));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProject(int id, AddingAndUpdatingProjectDto project, CancellationToken cancellationToken)
        {
            var spec = new ProjectSpecification(id);
            var updatingProject = await _projectService.GetProjectWithSpec(spec, cancellationToken);
            
            if (updatingProject == null)
            {
                return NotFound(new ApiResponse(404));
            }

            updatingProject.Group_Id = project.Group_Id;
            updatingProject.Name = project.Name;
            updatingProject.Customer = project.Customer;
            updatingProject.Status = project.Status;
            updatingProject.Start_Date = project.Start_Date;
            updatingProject.End_Date = project.End_Date;

            await _projectService.UpdateProjectAsync(updatingProject, cancellationToken);
            return Ok(new ApiResponse(200));
        }

        [HttpGet("getemployees/{id}")]
        public async Task<ActionResult<IReadOnlyCollection<EmployeeDto>>> GetEmployees(int id, CancellationToken cancellationToken)
        {
            var employees = await _projectService.GetEmployeesByProjectId(id, cancellationToken);
            return Ok(_mapper.Map<IReadOnlyCollection<Employee>, IReadOnlyCollection<EmployeeDto>>(employees));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProjects(string projectIds, CancellationToken cancellationToken)
        {
            int[] idsArray = projectIds.Split(',').Select(int.Parse).ToArray();

            List<Project> projects = new();
            foreach (var id in idsArray)
            {
                var spec = new ProjectSpecification(id);
                var project = await _projectService.GetProjectWithSpec(spec, cancellationToken);

                if (project == null) return NotFound(new ApiResponse(404));
                if (!project.Status.Equals("NEW")) return new BadRequestObjectResult(
                    new ApiValidationErrorResponse{Errors = new[]{"Delete projects with status 'New' only"}}
                    );

                projects.Add(project);
            }

            _projectService.DeleteProjects(projects.ToArray());
            await _projectService.SaveChangesAsync(cancellationToken);
            return Ok(new ApiResponse(200));
        }

        

        
    }
}