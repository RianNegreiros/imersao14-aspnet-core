using dotnet.Data;
using dotnet.DTOs;
using dotnet.Models;
using MongoDB.Driver;
using Prometheus;

namespace dotnet.Services
{
    public class RoutesDriverService
    {
        private readonly IMongoCollection<RouteDriver> _routesDriver;
        private readonly KafkaProducerService _kafkaProducerService;
        public RoutesDriverService(AppDbContext dbContext, KafkaProducerService kafkaProducerService)
        {
            _routesDriver = dbContext.RouteDrivers;
            _kafkaProducerService = kafkaProducerService;
        }

        public async Task CreateOrUpdateAsync(RoutesDriverDto routeDriverDto)
        {
            var countRouteDriver = await _routesDriver.CountDocumentsAsync(x => x.RouteId == routeDriverDto.RouteId);
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

            Counter routeStartedCounter = Metrics.CreateCounter("route_started_counter", "Route driver started route");
            Counter routeFinishedCounter = Metrics.CreateCounter("route_finished_counter", "Route driver finished route");

            if (countRouteDriver == 0)
            {
                routeStartedCounter.Inc();
                await _kafkaProducerService.ProduceAsync("route", "RouteStarted", data: new
                {
                    Id = routeDriverDto.RouteId,
                    StartAt = DateTime.UtcNow,
                    routeDriverDto.Lat,
                    routeDriverDto.Lng
                });
            }

            var lastPoint = await _routesDriver.Find(filter).Project(x => x.Points[-1]).FirstOrDefaultAsync();

            if (lastPoint.Location.Lat == routeDriverDto.Lat && lastPoint.Location.Lng == routeDriverDto.Lng)
            {
                routeFinishedCounter.Inc();
                await _kafkaProducerService.ProduceAsync("route", "RouteFinished", data: new
                {
                    Id = routeDriverDto.RouteId,
                    StopAt = DateTime.UtcNow,
                    routeDriverDto.Lat,
                    routeDriverDto.Lng
                });

                return;
            }

            await _kafkaProducerService.ProduceAsync("route", "DriverMoved", data: new
            {
                Id = routeDriverDto.RouteId,
                routeDriverDto.Lat,
                routeDriverDto.Lng
            });
        }
    }
}