using Newtonsoft.Json;

namespace DotNetApi.Models;
public class DirectionsData
{
    [JsonProperty("available_travel_modes")]
    public List<string> AvailableTravelModes { get; set; }
    [JsonProperty("geocoded_waypoints")]
    public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }
    public List<Route> Routes { get; set; }
    public DirectionsRequest Request { get; set; }
}

public class GeocodedWaypoint
{
    [JsonProperty("geocoder_status")]
    public string GeocoderStatus { get; set; }
    [JsonProperty("place_id")]
    public string PlaceId { get; set; }
    public List<string> Types { get; set; }
}

public class DirectionsRequest
{
    public RequestData Origin { get; set; }
    public RequestData Destination { get; set; }
}

public class RequestData {
    [JsonProperty("place_id")]
    public string PlaceId { get; set; }
    public Coord Location { get; set; }
}