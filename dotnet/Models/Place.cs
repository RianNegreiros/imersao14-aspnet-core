using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.Models;

public class Place
{
    [SwaggerSchema("Nome do local")]
    public string Name { get; set; }

    [SwaggerSchema("Coordenadas do local")]
    public Coord Location { get; set; }
}