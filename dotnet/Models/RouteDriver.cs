using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.Models;

public class RouteDriver
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [SwaggerSchema("Id do motorista")]
    public string Id { get; set; }

    [SwaggerSchema("Id da rota")]
    public string RouteId { get; set; }

    [SwaggerSchema("Pontos da rota")]
    public List<Point> Points { get; set; }

    [SwaggerSchema("Data de criação")]
    public DateTime CreatedAt { get; set; }

    [SwaggerSchema("Data de atualização")]
    public DateTime UpdatedAt { get; set; }
}

public class Point
{
    [SwaggerSchema("Coordenadas do ponto")]
    public Coord Location { get; set; }

    [SwaggerSchema("Data de criação")]
    public DateTime CreatedAt { get; set; }
}