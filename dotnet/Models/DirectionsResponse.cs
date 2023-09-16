using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

// Response sample: https://developers.google.com/maps/documentation/directions/get-directions#DirectionsResponses
namespace dotnet.Models;
public class DirectionsResponse
{
    [SwaggerSchema("Pontos geocodificadores")]
    [JsonProperty("geocoded_waypoints")]
    public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }

    [SwaggerSchema("Rotas")]
    public List<Route> Routes { get; set; }

    [SwaggerSchema("Status da requisição")]
    public string Status { get; set; }
}

public class Route
{
    [SwaggerSchema("Limites")]
    public Bounds Bounds { get; set; }

    [SwaggerSchema("Licença de uso")]
    public string Copyrights { get; set; }

    [SwaggerSchema("Legs")]
    public List<Leg> Legs { get; set; }

    [SwaggerSchema("Polyline")]
    [JsonProperty("overview_polyline")]
    public OverviewPolyline OverviewPolyline { get; set; }

    [SwaggerSchema("Sumário")]
    public string Summary { get; set; }

    [SwaggerSchema("Avisos")]
    public List<string> Warnings { get; set; }

    [SwaggerSchema("Ordem do ponto de passagem")]
    [JsonProperty("waypoint_order")]
    public List<object> WaypointOrder { get; set; }
}

public class Bounds
{
    [SwaggerSchema("Nordeste")]
    public Location Northeast { get; set; }

    [SwaggerSchema("Sudoeste")]
    public Location Southwest { get; set; }
}

public class Location
{
    [SwaggerSchema("Latitude")]
    public float Lat { get; set; }

    [SwaggerSchema("Longitude")]
    public float Lng { get; set; }
}

public class Leg
{
    [SwaggerSchema("Distância")]
    public Distance Distance { get; set; }

    [SwaggerSchema("Duração")]
    public Duration Duration { get; set; }

    [SwaggerSchema("Endereço final")]
    [JsonProperty("end_address")]
    public string EndAddress { get; set; }

    [SwaggerSchema("Localização final")]
    [JsonProperty("end_location")]
    public Location EndLocation { get; set; }

    [SwaggerSchema("Endereço inicial")]
    [JsonProperty("start_address")]
    public string StartAddress { get; set; }

    [SwaggerSchema("Localização inicial")]
    [JsonProperty("start_location")]
    public Location StartLocation { get; set; }

    [SwaggerSchema("Passos")]
    public List<Step> Steps { get; set; }

    [SwaggerSchema("Entrada de velocidade de tráfego")]
    [JsonProperty("traffic_speed_entry")]
    public List<object> TrafficSpeedEntry { get; set; }

    [SwaggerSchema("Via de ponto de passagem")]
    [JsonProperty("via_waypoint")]
    public List<object> ViaWaypoint { get; set; }
}

public class Distance
{
    [SwaggerSchema("Texto")]
    public string Text { get; set; }

    [SwaggerSchema("Valor")]
    public int Value { get; set; }
}

public class Duration
{
    [SwaggerSchema("Texto")]
    public string Text { get; set; }

    [SwaggerSchema("Valor")]
    public int Value { get; set; }
}

public class Step
{
    [SwaggerSchema("Distância")]
    public Distance Distance { get; set; }

    [SwaggerSchema("Duração")]
    public Duration Duration { get; set; }

    [SwaggerSchema("Endereço final")]
    [JsonProperty("end_location")]
    public Location EndLocation { get; set; }

    [SwaggerSchema("Instruções em HTML")]
    [JsonProperty("html_instructions")]
    public string HtmlInstructions { get; set; }

    [SwaggerSchema("Manobra")]
    public string Maneuver { get; set; }

    [SwaggerSchema("Polyline")]
    public Polyline Polyline { get; set; }

    [SwaggerSchema("Endereço inicial")]
    [JsonProperty("start_location")]
    public Location StartLocation { get; set; }

    [SwaggerSchema("Modo de viagem")]
    [JsonProperty("travel_mode")]
    public string TravelMode { get; set; }
}

public class Polyline
{
    [SwaggerSchema("Pontos")]
    public string Points { get; set; }
}

public class OverviewPolyline
{
    [SwaggerSchema("Pontos")]
    public string Points { get; set; }
}