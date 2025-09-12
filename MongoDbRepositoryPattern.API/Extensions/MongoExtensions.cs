using MongoDB.Driver;
using MongoDbRepositoryPattern.API.Abstractions;
using MongoDbRepositoryPattern.API.Models;
using MongoDbRepositoryPattern.API.Mongo;

namespace MongoDbRepositoryPattern.API.Extensions;

public static class MongoExtensions
{
    public static IServiceCollection AddMongoRepositories(this IServiceCollection services, IConfiguration config)
    {
        var conn = config.GetConnectionString("Mongo") ?? "mongodb://localhost:27017";
        var dbName = config["Database:Name"] ?? "Db";

        services.AddSingleton<IMongoClient>(_ => new MongoClient(conn));
        services.AddSingleton(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(dbName));

        var usersCollection = config["Database:Collections:Users"] ?? "Users";
        services.AddScoped<IRepository<User>>(sp =>
            new MongoRepository<User>(sp.GetRequiredService<IMongoDatabase>(), usersCollection));

        return services;
    }
}
