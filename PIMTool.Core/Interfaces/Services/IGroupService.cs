using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Specifications;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IGroupService
    {
        Task<IReadOnlyCollection<Group>> GetGroupsAsyncWithSpec(ISpecification<Group> spec, CancellationToken cancellationToken);

        Task<Group?> GetGroupWithSpec(ISpecification<Group> spec, CancellationToken cancellationToken);

        Task AddRangeGroupAsync(IEnumerable<Group> groups, CancellationToken cancellationToken = default);

        Task AddGroupAsync(Group group, CancellationToken cancellationToken = default);

        Task UpdateGroupAsync(Group group, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
