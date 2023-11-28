using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.AddingAndUpdatingDtos;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Dtos;
using PIMTool.Errors;
using PIMTool.Helpers;

namespace PIMTool.Controllers
{
    public class GroupsController : BaseApiController
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<GroupDto>>> GetGroupsAsync([FromQuery] GroupSpecParams groupParams,
            CancellationToken cancellationToken)
        {
            var spec = new GroupSpecification(groupParams);

            var countSpec = new GroupWithFilterForCountSpecification(groupParams);

            var totalItems = await _groupService.CountGroupsAsync(countSpec);

            var groups = await _groupService.GetGroupsAsyncWithSpec(spec, cancellationToken);

            var data = _mapper.Map<IReadOnlyCollection<Group>, IReadOnlyCollection<GroupDto>>(groups);

            return Ok(new Pagination<GroupDto>(groupParams.PageIndex, groupParams.PageSize, totalItems, data));
        }

        [HttpGet("with-projects/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupDto>> GetGroupAsyncWithProjects(int id, CancellationToken cancellationToken)
        {
            var spec = new GroupSpecification(id);
            var group = await _groupService.GetGroupWithSpec(spec, cancellationToken);
            if (group == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<Group, GroupDto>(group);
        }


        [HttpGet("without-projects/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupWithoutProjectsDto>> GetGroupAsyncWithoutProjects(int id, CancellationToken cancellationToken)
        {
            var spec = new GroupSpecification(id);
            var group = await _groupService.GetGroupWithSpec(spec, cancellationToken);
            if (group == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<Group, GroupWithoutProjectsDto>(group);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup([FromBody] AddingAndUpdatingGroupDto group, CancellationToken cancellationToken)
        {
            Group newGroup = new()
            {
                Group_Leader_Id = group.Group_Leader_Id
            };
            await _groupService.AddGroupAsync(newGroup, cancellationToken);
            return Ok(new ApiResponse(200));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGroup(int id, AddingAndUpdatingGroupDto group, CancellationToken cancellationToken)
        {
            var spec = new GroupSpecification(id);
            var updatingGroup = await _groupService.GetGroupWithSpec(spec, cancellationToken);

            if (updatingGroup == null)
            {
                return NotFound(new ApiResponse(404));
            }

            updatingGroup.Group_Leader_Id = group.Group_Leader_Id;

            await _groupService.UpdateGroupAsync(updatingGroup, cancellationToken);
            return Ok(new ApiResponse(200));
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGroup(int id, CancellationToken cancellationToken)
        {
            var spec = new GroupSpecification(id);
            var group = await _groupService.GetGroupWithSpec(spec, cancellationToken);
            if (group == null)
            {
                return NotFound(new ApiResponse(404));
            }
            await _groupService.DeleteGroup(group, cancellationToken);
            return Ok(new ApiResponse(200));
        }
    }
}
