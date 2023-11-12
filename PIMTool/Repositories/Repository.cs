using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Specifications;
using PIMTool.Database;
using PIMTool.SpecificationEvaluator;

namespace PIMTool.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly PimContext _pimContext;
        private readonly DbSet<T> _set;

        public Repository(PimContext pimContext)
        {
            _pimContext = pimContext;
            _set = _pimContext.Set<T>();
        }

        public async Task<IReadOnlyCollection<T>> GetAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<T?> GetIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _set.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _set.AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _set.AddAsync(entity, cancellationToken);
            await SaveChangesAsync();
        }

        public async Task Delete(T[] entities, CancellationToken cancellationToken)
        {
            _set.RemoveRange(entities);
            await SaveChangesAsync(cancellationToken);
        }


        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _pimContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<T>> GetAsyncWithSpec(ISpecification<T> spec, CancellationToken cancellationToken)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _set.Update(entity);
            await SaveChangesAsync(cancellationToken);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_set.AsQueryable(), spec);
        }

        
    }
}