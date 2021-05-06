using Blog.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
    public interface IAsyncRepository<T> where T : Entity
    {
        ValueTask<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        ValueTask<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);
        ValueTask<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        ValueTask<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
