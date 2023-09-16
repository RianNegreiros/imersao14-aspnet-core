using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.DTOs;
public class RoutesDriverDto
{
    [SwaggerSchema("Id da rota")]
    public string RouteId { get; set; }

    [SwaggerSchema("Latitude")]
    public float Lat { get; set; }

    [SwaggerSchema("Longitude")]
    public float Lng { get; set; }
}
