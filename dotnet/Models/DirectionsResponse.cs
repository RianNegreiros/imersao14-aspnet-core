using Newtonsoft.Json;

// Response sample: https://developers.google.com/maps/documentation/directions/get-directions#DirectionsResponses
namespace dotnet.Models;
public class DirectionsResponse
{
    [JsonProperty("geocoded_waypoints")]
    public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }
    public List<Route> Routes { get; set; }
    public string Status { get; set; }
}

public class Route
{
    public Bounds Bounds { get; set; }
    public string Copyrights { get; set; }
    public List<Leg> Legs { get; set; }
    [JsonProperty("overview_polyline")]
    public OverviewPolyline OverviewPolyline { get; set; }
    public string Summary { get; set; }
    public List<string> Warnings { get; set; }
    [JsonProperty("waypoint_order")]
    public List<object> WaypointOrder { get; set; }
}

public class Bounds
{
    public Location Northeast { get; set; }
    public Location Southwest { get; set; }
}

public class Location
{
    public float Lat { get; set; }
    public float Lng { get; set; }
}

public class Leg
{
    public Distance Distance { get; set; }
    public Duration Duration { get; set; }
    [JsonProperty("end_address")]
    public string EndAddress { get; set; }
    [JsonProperty("end_location")]
    public Location EndLocation { get; set; }
    [JsonProperty("start_address")]
    public string StartAddress { get; set; }
    [JsonProperty("start_location")]
    public Location StartLocation { get; set; }
    public List<Step> Steps { get; set; }
    [JsonProperty("traffic_speed_entry")]
    public List<object> TrafficSpeedEntry { get; set; }
    [JsonProperty("via_waypoint")]
    public List<object> ViaWaypoint { get; set; }
}

public class Distance
{
    public string Text { get; set; }
    public int Value { get; set; }
}

public class Duration
{
    public string Text { get; set; }
    public int Value { get; set; }
}

public class Step
{
    public Distance Distance { get; set; }
    public Duration Duration { get; set; }
    [JsonProperty("end_location")]
    public Location EndLocation { get; set; }
    [JsonProperty("html_instructions")]
    public string HtmlInstructions { get; set; }
    public string Maneuver { get; set; }
    public Polyline Polyline { get; set; }
    [JsonProperty("start_location")]
    public Location StartLocation { get; set; }
    [JsonProperty("travel_mode")]
    public string TravelMode { get; set; }
}

public class Polyline
{
    public string Points { get; set; }
}

public class OverviewPolyline
{
    public string Points { get; set; }
}