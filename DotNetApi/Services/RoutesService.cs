using System.Text.Json;
using DotNetApi.Data;
using DotNetApi.DTOs;
using DotNetApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNetApi.Services;
public class RoutesService
{
    private readonly AppDbContext _dbContext;

    public RoutesService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RouteModel> CreateRouteAsync(CreateRouteDto routeDto)
    {
        var route = new RouteModel
        {
            Name = routeDto.Name,
            Source = new Place
            {
                Name = routeDto.SourceId,
                Location = new Coord
                {
                    Lat = 0,
                    Lng = 0
                },
            },
            Destination = new Place
            {
                Name = routeDto.DestinationId,
                Location = new Coord
                {
                    Lat = 0,
                    Lng = 0
                },
            },
            Distance = 0,
            Duration = 0,
            Directions = BsonDocument.Parse("{}")
        };

        await _dbContext.Routes.InsertOneAsync(route);

        return route;
    }

    public async Task<List<RouteModel>> FindAllAsync()
    {
        var routes = await _dbContext.Routes.Find(_ => true).ToListAsync();

        return routes;
    }
}
