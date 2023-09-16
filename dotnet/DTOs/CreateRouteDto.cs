using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.DTOs;

public class CreateRouteDto
{
    [SwaggerSchema("Nome da rota")]
    public string Name { get; set; }

    [SwaggerSchema("Id da destinação")]
    public string DestinationId { get; set; }

    [SwaggerSchema("Id da origem")]
    public string SourceId { get; set; }
}
