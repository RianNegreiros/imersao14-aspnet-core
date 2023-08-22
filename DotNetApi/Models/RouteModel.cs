using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetApi.Models;
public class RouteModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public Place Source { get; set; }
    public Place Destination { get; set; }
    public float Distance { get; set; }
    public float Duration { get; set; }
    [BsonElement("directions")]
    public DirectionsData Directions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<RouteDriver> RouteDrivers { get; set; }
}

public class DirectionsData
{
    public List<Route> Routes { get; set; }
    [BsonElement("geocoded_waypoints")]
    public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }
    [BsonElement("available_travel_modes")]
    public List<string> AvailableTravelModes { get; set; }
    
    public string Status { get; set; }
}