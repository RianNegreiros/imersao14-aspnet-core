using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.Models;

// https://developers.google.com/maps/documentation/places/web-service/search-find-place#find-place-responses
public class PlaceResponse
{
  [SwaggerSchema("Candidatos a local")]
  public List<Candidate> Candidates { get; set; }

  [SwaggerSchema("Status da requisição")]
  public string Status { get; set; }
}

public class Candidate
{
  [SwaggerSchema("Endereço formatado")]
  [JsonProperty("formatted_address")]
  public string FormattedAddress { get; set; }

  [SwaggerSchema("Geometria")]
  public Geometry Geometry { get; set; }

  [SwaggerSchema("Nome do local")]
  public string Name { get; set; }

  [SwaggerSchema("Id do local")]
  [JsonProperty("place_id")]
  public string PlaceId { get; set; }

  [SwaggerSchema("Classificação do local")]
  public float Rating { get; set; }
}

public class Geometry
{
  [SwaggerSchema("Localização")]
  public Location Location { get; set; }

  [SwaggerSchema("Viewport")]
  public Viewport Viewport { get; set; }
}

public class Viewport
{
  [SwaggerSchema("Nordeste")]
  public Northeast Northeast { get; set; }

  [SwaggerSchema("Sudoeste")]
  public Southwest Southwest { get; set; }
}

public class Northeast
{
  [SwaggerSchema("Latitude")]
  public float Lat { get; set; }

  [SwaggerSchema("Longitude")]
  public float Lng { get; set; }
}

public class Southwest
{
  [SwaggerSchema("Latitude")]
  public float Lat { get; set; }

  [SwaggerSchema("Longitude")]
  public float Lng { get; set; }
}
