using DotNetApi.Services;
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

    [HttpGet]
    public async Task<IActionResult> GetDirections([FromQuery] string originId, [FromQuery] string destinationId)
    {
        try
        {
            var directionsResponse = await _googleMapsService.GetDirectionsAsync(originId, destinationId);
            if (directionsResponse != null)
            {
                return Content(directionsResponse, "application/json");
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}