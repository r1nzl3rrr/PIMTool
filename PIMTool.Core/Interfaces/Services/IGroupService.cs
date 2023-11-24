using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Specifications;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IGroupService
    {
        Task<IReadOnlyCollection<Group>> GetGroupsAsyncWithSpec(ISpecification<Group> spec, CancellationToken cancellationToken);

        Task<Group?> GetGroupWithSpec(ISpecification<Group> spec, CancellationToken cancellationToken);

        Task<int> CountGroupsAsync(ISpecification<Group> spec, CancellationToken cancellationToken = default);

        Task AddGroupAsync(Group group, CancellationToken cancellationToken = default);

        Task AddRangeGroupAsync(IList<Group> groups);

        Task UpdateGroupAsync(Group group, CancellationToken cancellationToken = default);

        Task DeleteGroup(Group group, CancellationToken cancellationToken);

    }
}
