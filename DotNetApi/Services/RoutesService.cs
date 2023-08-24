using DotNetApi.Data;
using DotNetApi.DTOs;
using DotNetApi.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DotNetApi.Services;
public class RoutesService
{
    private readonly IMongoCollection<RouteModel> _routesCollection;
    private readonly GoogleMapsService _googleMapsService;
    private readonly KafkaProducerService _kafkaProducerService;
    public RoutesService(AppDbContext dbContext, GoogleMapsService googleMapsService, KafkaProducerService kafkaProducerService)
    {
        _routesCollection = dbContext.Routes;
        _googleMapsService = googleMapsService;
        _kafkaProducerService = kafkaProducerService;
    }

    public async Task<RouteModel> CreateRouteAsync(CreateRouteDto createRouteDto)
    {
        var directionsResponseJson = await _googleMapsService.GetDirectionsAsync(createRouteDto.SourceId, createRouteDto.DestinationId);
        var directionsResponse = JsonConvert.DeserializeObject<DirectionsResponse>(directionsResponseJson);
        if (directionsResponse == null)
        {
            return null;
        }

        var legs = directionsResponse.Routes.First().Legs.First();
        var route = new RouteModel
        {
            Name = createRouteDto.Name,
            Source = new Place
            {
                Name = legs.StartAddress,
                Location = new Coord
                {
                    Lat = legs.StartLocation.Lat,
                    Lng = legs.StartLocation.Lng
                }
            },
            Destination = new Place
            {
                Name = legs.EndAddress,
                Location = new Coord
                {
                    Lat = legs.EndLocation.Lat,
                    Lng = legs.EndLocation.Lng
                }
            },
            Distance = legs.Distance.Value,
            Duration = legs.Duration.Value,
            Directions = new DirectionsData
            {
                AvailableTravelModes = directionsResponse.Routes.First().Legs.First().Steps.Select(s => s.TravelMode).Distinct().ToList(),
                GeocodedWaypoints = directionsResponse.GeocodedWaypoints,
                Routes = directionsResponse.Routes,
                Request = new DirectionsRequest
                {
                    Origin = new RequestData
                    {
                        PlaceId = createRouteDto.SourceId,
                        Location = new Coord
                        {
                            Lat = legs.StartLocation.Lat,
                            Lng = legs.StartLocation.Lng
                        }
                    },
                    Destination = new RequestData
                    {
                        PlaceId = createRouteDto.DestinationId,
                        Location = new Coord
                        {
                            Lat = legs.EndLocation.Lat,
                            Lng = legs.EndLocation.Lng
                        }
                    }
                }
            },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _routesCollection.InsertOneAsync(route);

        await _kafkaProducerService.ProduceAsync(
            topic: "route",
            eventName: "RouteCreated",
            data: new
            {
                route.Id,
                route.Name,
                route.Source,
                route.Destination,
                route.Distance,
                route.Duration,
                route.Directions
            }
        );

        return route;
    }

    public async Task<RouteModel> FindOneAsync(string id)
    {
        var filter = Builders<RouteModel>.Filter.Eq(r => r.Id, id);
        return await _routesCollection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<RouteModel>> FindAllAsync()
    {
        return await _routesCollection.AsQueryable().ToListAsync();
    }

    public async Task<RouteModel> UpdateAsync(string id, UpdateRouteDto updateRouteDto)
    {
        var filter = Builders<RouteModel>.Filter.Eq(r => r.Id, id);
        var existingRoute = await _routesCollection.Find(filter).FirstOrDefaultAsync();

        if (existingRoute == null)
        {
            return null;
        }

        await _routesCollection.ReplaceOneAsync(filter, existingRoute);

        return existingRoute;
    }

    public async Task<bool> RemoveAsync(string id)
    {
        var result = await _routesCollection.DeleteOneAsync(r => r.Id == id);

        return result.DeletedCount > 0;
    }
}
