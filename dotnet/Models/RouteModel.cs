using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.Models;

public class RouteModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [SwaggerSchema("Id da rota")]
    public string Id { get; set; }

    [SwaggerSchema("Nome da rota")]
    public string Name { get; set; }

    [SwaggerSchema("Origem")]
    public Place Source { get; set; }

    [SwaggerSchema("Destinação")]
    public Place Destination { get; set; }

    [SwaggerSchema("Distância")]
    public float Distance { get; set; }

    [SwaggerSchema("Duração")]
    public float Duration { get; set; }

    [SwaggerSchema("Direções")]
    [BsonElement("directions")]
    public DirectionsData Directions { get; set; }

    [SwaggerSchema("Data de criação")]
    public DateTime CreatedAt { get; set; }

    [SwaggerSchema("Data de atualização")]
    public DateTime UpdatedAt { get; set; }

    [SwaggerSchema("Rotas de motoristas")]
    public List<RouteDriver> RouteDrivers { get; set; }
}
