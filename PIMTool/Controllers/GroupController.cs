using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;
using PIMTool.Services;

namespace PIMTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Group>>> GetGroupsAsync(CancellationToken cancellationToken)
        {
            var spec = new GroupSpecification();
            var groups = await _groupService.GetGroupsAsyncWithSpec(spec, cancellationToken);
            return Ok(groups);
        }

    }
}
