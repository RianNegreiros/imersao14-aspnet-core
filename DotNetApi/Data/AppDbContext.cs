using DotNetApi.Models;
using MongoDB.Driver;

namespace DotNetApi.Data;
public class AppDbContext
{
    private readonly IMongoDatabase _database;

    public AppDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
        _database = client.GetDatabase("dotnet");
    }

    public IMongoCollection<RouteModel> Routes => _database.GetCollection<RouteModel>("routes");
    public IMongoCollection<RouteDriver> RouteDrivers => _database.GetCollection<RouteDriver>("routeDrivers");
}