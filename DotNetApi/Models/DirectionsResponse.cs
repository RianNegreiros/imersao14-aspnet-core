using Newtonsoft.Json;

// Response sample: https://developers.google.com/maps/documentation/directions/get-directions#DirectionsResponses
namespace DotNetApi.Models;
public class DirectionsResponse
{
    [JsonProperty("geocoded_waypoints")]
    public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }
    public List<Route> Routes { get; set; }
    public string Status { get; set; }
}

public class Route
{
    public List<Leg> Legs { get; set; }
}

public class Leg
{
    [JsonProperty("start_address")]
    public string StartAddress { get; set; }
    [JsonProperty("start_location")]
    public Location StartLocation { get; set; }
    [JsonProperty("end_address")]
    public string EndAddress { get; set; }
    [JsonProperty("end_location")]
    public Location EndLocation { get; set; }
    public Distance Distance { get; set; }
    public Duration Duration { get; set; }
    public List<Step> Steps { get; set; }
}

public class GeocodedWaypoint
{
    [JsonProperty("geocoder_status")]
    public string GeocoderStatus { get; set; }
    [JsonProperty("place_id")]
    public string PlaceId { get; set; }
    public List<string> Types { get; set; }
}

public class Step
{
    [JsonProperty("travel_mode")]
    public string TravelMode { get; set; }
}

public class Distance
{
    public int Value { get; set; }
}

public class Duration
{
    public int Value { get; set; }
}