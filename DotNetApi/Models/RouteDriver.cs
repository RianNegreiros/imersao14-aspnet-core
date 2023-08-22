using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetApi.Models;
public class RouteDriver
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string RouteId { get; set; }
    public List<Point> Points { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
