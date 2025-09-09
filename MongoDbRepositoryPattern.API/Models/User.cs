using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbRepositoryPattern.API.Abstractions;

namespace MongoDbRepositoryPattern.API.Models;

public class User : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
}
