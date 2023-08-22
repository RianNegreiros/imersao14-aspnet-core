using DotNetApi.GoogleMaps;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DirectionsController : ControllerBase
{
    private readonly GoogleMapsService _googleMapsService;

    public DirectionsController(GoogleMapsService googleMapsService)
    {
        _googleMapsService = googleMapsService;
    }

    [HttpGet("getDirections")]
    public async Task<IActionResult> GetDirections([FromQuery] string placeOriginId, [FromQuery] string placeDestinationId)
    {
        try
        {
            var directionsResponse = await _googleMapsService.GetDirectionsAsync(placeOriginId, placeDestinationId);
            return Content(directionsResponse, "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
