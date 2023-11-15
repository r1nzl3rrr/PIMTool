using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Dtos;
using PIMTool.Services;

namespace PIMTool.Controllers
{
    public class GroupController : BaseApiController
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }


        [HttpGet("with-projects/{id}")]
        public async Task<ActionResult<GroupDto>> GetGroupAsyncWithProjects(int id, CancellationToken cancellationToken)
        {
            var spec = new GroupSpecification(id);
            var group = await _groupService.GetGroupWithSpec(spec, cancellationToken);
            return _mapper.Map<Group, GroupDto>(group);
        }


        [HttpGet("without-projects/{id}")]
        public async Task<ActionResult<GroupWithoutProjectsDto>> GetGroupAsyncWithoutProjects(int id, CancellationToken cancellationToken)
        {
            var spec = new GroupSpecification(id);
            var group = await _groupService.GetGroupWithSpec(spec, cancellationToken);
            return _mapper.Map<Group, GroupWithoutProjectsDto>(group);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(Group group, CancellationToken cancellationToken)
        {
            await _groupService.AddGroupAsync(group, cancellationToken);
            return Ok();
        }

        [HttpPost("range")]
        public async Task<IActionResult> AddRangeGroup(IEnumerable<Group> groups, CancellationToken cancellationToken)
        {
            await _groupService.AddRangeGroupAsync(groups, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, CancellationToken cancellationToken)
        {
            var spec = new GroupSpecification(id);
            var group = await _groupService.GetGroupWithSpec(spec, cancellationToken);
            if (group == null)
            {
                return NotFound();
            }
            await _groupService.UpdateGroupAsync(group, cancellationToken);
            return Ok();
        }

    }
}
