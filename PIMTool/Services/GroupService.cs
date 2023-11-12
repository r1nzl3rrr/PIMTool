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
            await SaveChangesAsync();
        }

        public async Task AddRangeGroupAsync(IEnumerable<Group> groups, CancellationToken cancellationToken = default)
        {
            await _groupRepo.AddRangeAsync(groups, cancellationToken);
            await SaveChangesAsync();
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
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _groupRepo.SaveChangesAsync(cancellationToken);
        }
    }
}
