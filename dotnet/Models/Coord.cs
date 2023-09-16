using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.Models;
public class Coord
{
    [SwaggerSchema("Latitude")]
    public float Lat { get; set; }

    [SwaggerSchema("Longitude")]
    public float Lng { get; set; }
}