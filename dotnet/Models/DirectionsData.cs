using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.Models;
public class DirectionsData
{
    [SwaggerSchema("Modos de viagem disponíveis")]
    [JsonProperty("available_travel_modes")]
    public List<string> AvailableTravelModes { get; set; }

    [SwaggerSchema("Pontos geocodificadores")]
    [JsonProperty("geocoded_waypoints")]
    public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }

    [SwaggerSchema("Rotas")]
    public List<Route> Routes { get; set; }

    [SwaggerSchema("Direções")]
    public DirectionsRequest Request { get; set; }
}

public class GeocodedWaypoint
{
    [SwaggerSchema("Status do geocodificador")]
    [JsonProperty("geocoder_status")]
    public string GeocoderStatus { get; set; }

    [SwaggerSchema("Id do local")]
    [JsonProperty("place_id")]
    public string PlaceId { get; set; }

    [SwaggerSchema("Tipos de local")]
    public List<string> Types { get; set; }
}

public class DirectionsRequest
{
    [SwaggerSchema("Origem")]
    public RequestData Origin { get; set; }

    [SwaggerSchema("Destino")]
    public RequestData Destination { get; set; }
}

public class RequestData {
    [SwaggerSchema("Id do local")]
    [JsonProperty("place_id")]
    public string PlaceId { get; set; }

    [SwaggerSchema("Localização")]
    public Coord Location { get; set; }
}