namespace dotnet.Services;
public class GoogleMapsService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public GoogleMapsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("GoogleMapsClient");
        _configuration = configuration;
    }

    public async Task<string> FindPlaceAsync(string text)
    {
        string apiUrl = $"https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input={Uri.EscapeDataString(text)}" +
                        $"&inputtype=textquery&fields=place_id,formatted_address,geometry,name" +
                        $"&key={_configuration["GoogleMaps:API_KEY"]}";

        var response = await _httpClient.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        else
        {
            throw new Exception($"Google Places API request failed with status code {response.StatusCode}");
        }
    }

    public async Task<string> GetDirectionsAsync(string originPlaceId, string destinationPlaceId)
    {
        string apiUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin=place_id:{originPlaceId}&destination=place_id:{destinationPlaceId}&mode=driving&key={_configuration["GoogleMaps:API_KEY"]}";

        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        return null;
    }
}