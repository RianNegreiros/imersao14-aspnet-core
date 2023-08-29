using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnet.Models;
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
