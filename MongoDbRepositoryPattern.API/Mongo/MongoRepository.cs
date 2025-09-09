using MongoDB.Driver;
using MongoDbRepositoryPattern.API.Abstractions;
using System.Linq.Expressions;

namespace MongoDbRepositoryPattern.API.Mongo;

public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _collection = default!;
    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public Task<T> GetByIdAsync(string id, CancellationToken ct = default)
        => _collection.Find(x => x.Id == id).FirstOrDefaultAsync(ct);

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? filter = null,
                                             int? skip = null, int? take = null,
                                             CancellationToken ct = default)
    {
        var find = _collection.Find(filter ?? (_ => true));
        if (skip is { } s) find = find.Skip(s);
        if (take is { } t) find = find.Limit(t);
        return await find.ToListAsync(ct);
    }

    public Task InsertAsync(T entity, CancellationToken ct = default)
    => _collection.InsertOneAsync(entity, cancellationToken: ct);

    public Task ReplaceAsync(T entity, CancellationToken ct = default)
    => _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: ct);

    public Task DeleteAsync(string id, CancellationToken ct = default)
        => _collection.DeleteOneAsync(x => x.Id == id, ct);
}
