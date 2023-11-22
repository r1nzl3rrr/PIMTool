using PIMTool.Core.Specifications;
using System.Reflection.Metadata;

namespace PIMTool.Core.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<IReadOnlyCollection<T>> GetAsync();

        Task<IReadOnlyCollection<T>> GetAsyncWithSpec(ISpecification<T> spec, CancellationToken cancellationToken);

        Task<T?> GetIdAsync(int id, CancellationToken cancellationToken = default);

        Task<T?> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken);
        Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task Delete(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}