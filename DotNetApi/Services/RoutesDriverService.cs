using DotNetApi.Data;
using DotNetApi.DTOs;
using DotNetApi.Models;
using MongoDB.Driver;

namespace DotNetApi.Services
{
    public class RoutesDriverService
    {
        private readonly IMongoCollection<RouteDriver> _routesDriver;
        public RoutesDriverService(AppDbContext dbContext)
        {
            _routesDriver = dbContext.RouteDrivers;
        }

        public async Task CreateOrUpdateAsync(RoutesDriverDto routeDriverDto)
        {
            var filter = Builders<RouteDriver>.Filter.Eq(x => x.RouteId, routeDriverDto.RouteId);

            var newPoint = new Point
            {
                Location = new Coord
                {
                    Lat = routeDriverDto.Lat,
                    Lng = routeDriverDto.Lng
                },
                CreatedAt = DateTime.UtcNow
            };

            var update = Builders<RouteDriver>.Update.Push(x => x.Points, newPoint)
                .Set(x => x.UpdatedAt, DateTime.UtcNow);

            var options = new UpdateOptions { IsUpsert = true };
            await _routesDriver.UpdateOneAsync(filter, update, options);
        }
    }
}