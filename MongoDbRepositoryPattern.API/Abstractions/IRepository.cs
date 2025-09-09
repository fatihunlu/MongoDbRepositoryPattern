using System.Linq.Expressions;

namespace MongoDbRepositoryPattern.API.Abstractions;

public interface IRepository<T> where T : IEntity
{
    Task<T> GetByIdAsync(string id, CancellationToken ct = default);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? filter = null,
                                    int? skip = null, int? take = null,
                                    CancellationToken ct = default);
    Task InsertAsync(T entity, CancellationToken ct = default);
    Task ReplaceAsync(T entity, CancellationToken ct = default);
    Task DeleteAsync(string id, CancellationToken ct = default);
}
