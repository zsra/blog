using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repository
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : Entity
    {
        internal readonly BlogDbContext _context;

        public AsyncRepository(BlogDbContext context) => _context = context;

        public async ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            TEntity _entity = await GetByIdAsync(id, cancellationToken);
            _context.Set<TEntity>().Remove(_entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async ValueTask<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await _context.Set<TEntity>().FindAsync(keyValues, cancellationToken);
        }

        public async ValueTask<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return await GetByIdAsync(entity.Id);
        }
    }
}
