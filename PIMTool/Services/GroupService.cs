using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;

namespace PIMTool.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _groupRepo;

        public GroupService(IRepository<Group> groupRepo)
        {
            _groupRepo = groupRepo;
        }

        public async Task AddGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            await _groupRepo.AddAsync(group, cancellationToken);
        }

        public async Task AddRangeGroupAsync(IList<Group> groups)
        {
            await _groupRepo.AddRangeAsync(groups);
        }

        public async Task<IReadOnlyCollection<Group>> GetGroupsAsyncWithSpec(ISpecification<Group> spec, CancellationToken cancellationToken)
        {
            return await _groupRepo.GetAsyncWithSpec(spec, cancellationToken);
        }

        public async Task<Group?> GetGroupWithSpec(ISpecification<Group> spec, CancellationToken cancellationToken)
        {
            return await _groupRepo.GetEntityWithSpec(spec, cancellationToken);
        }
        public async Task UpdateGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            await _groupRepo.UpdateAsync(group, cancellationToken);
        }

        public async Task DeleteGroup(Group group, CancellationToken cancellationToken)
        {
            await _groupRepo.Delete(group, cancellationToken);
        }

        public async Task<int> CountGroupsAsync(ISpecification<Group> spec, CancellationToken cancellationToken = default)
        {
            return await _groupRepo.CountAsync(spec, cancellationToken);
        }
        
    }
}
