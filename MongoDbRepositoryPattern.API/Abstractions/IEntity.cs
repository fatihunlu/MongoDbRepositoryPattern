using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbRepositoryPattern.API.Abstractions;

public interface IEntity
{
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    string Id { get; set; }
}
