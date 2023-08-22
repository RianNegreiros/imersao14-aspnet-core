using Newtonsoft.Json;

namespace DotNetApi.GoogleMaps;

// https://developers.google.com/maps/documentation/places/web-service/search-find-place#find-place-responses
public class GoogleMapsPlaceResponse
{
  public List<Candidate> Candidates { get; set; }
  public string Status { get; set; }
}

public class Candidate
{
  [JsonProperty("formatted_address")]
  public string FormattedAddress { get; set; }
  public Geometry Geometry { get; set; }
  public string Name { get; set; }
  [JsonProperty("place_id")]
  public string PlaceId { get; set; }
  public double Rating { get; set; }
}

public class Geometry
{
  public Location Location { get; set; }
  public Viewport Viewport { get; set; }
}

public class Location
{
  public double Lat { get; set; }
  public double Lng { get; set; }
}

public class Viewport
{
  public Northeast Northeast { get; set; }
  public Southwest Southwest { get; set; }
}

public class Northeast
{
  public double Lat { get; set; }
  public double Lng { get; set; }
}

public class Southwest
{
  public double Lat { get; set; }
  public double Lng { get; set; }
}
