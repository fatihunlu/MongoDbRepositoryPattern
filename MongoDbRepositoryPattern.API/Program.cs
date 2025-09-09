using MongoDB.Driver;
using MongoDbRepositoryPattern.API.Abstractions;
using MongoDbRepositoryPattern.API.Models;
using MongoDbRepositoryPattern.API.Mongo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("Mongo") ?? "mongodb://localhost:27017";
var dbName = builder.Configuration["Database:Name"] ?? "Db";

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(conn));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(dbName));

var usersCollection = builder.Configuration["Database:Collections:Users"] ?? "Users";
builder.Services.AddScoped<IRepository<User>>(sp =>
    new MongoRepository<User>(sp.GetRequiredService<IMongoDatabase>(), usersCollection));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
