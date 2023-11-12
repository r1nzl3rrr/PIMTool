using PIMTool.Core.Specifications;

namespace PIMTool.Core.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<IReadOnlyCollection<T>> GetAsync();

        Task<IReadOnlyCollection<T>> GetAsyncWithSpec(ISpecification<T> spec, CancellationToken cancellationToken);

        Task<T?> GetIdAsync(int id, CancellationToken cancellationToken = default);

        Task<T?> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task Delete(T[] entities, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}