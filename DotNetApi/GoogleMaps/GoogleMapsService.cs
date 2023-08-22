namespace DotNetApi.GoogleMaps;
public class GoogleMapsService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public GoogleMapsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("GoogleMapsClient");
        _configuration = configuration;
    }

    public async Task<GoogleMapsPlaceResponse> FindPlaceAsync(string text)
    {
        string apiUrl = $"https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input={Uri.EscapeDataString(text)}" +
                        $"&inputtype=textquery&fields=place_id,formatted_address,geometry,name" +
                        $"&key={_configuration["GoogleMaps:API_KEY"]}";

        var response = await _httpClient.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var placeResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleMapsPlaceResponse>(content);
            return placeResponse;
        }
        else
        {
            throw new Exception($"Google Places API request failed with status code {response.StatusCode}");
        }
    }

    // Response may be too large, perhaps not worth typing: https://developers.google.com/maps/documentation/directions/get-directions#DirectionsResponses
    public async Task<string> GetDirectionsAsync(string placeOriginId, string placeDestinationId)
    {
        var response = await _httpClient.GetAsync(
            $"https://maps.googleapis.com/maps/api/directions/json?origin=place_id:{placeOriginId}&destination=place_id:{placeDestinationId}&mode=driving&key={_configuration["GoogleMaps:API_KEY"]}",
            HttpCompletionOption.ResponseContentRead
        );

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        else
        {
            throw new Exception($"Google Maps Directions API request failed with status code {response.StatusCode}");
        }
    }
}
