using dotnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly GoogleMapsService _placesService;

    public PlacesController(GoogleMapsService placesService)
    {
        _placesService = placesService;
    }

    [HttpGet]
    public async Task<IActionResult> FindPlace([FromQuery] string text)
    {
        try
        {
            var placeResponse = await _placesService.FindPlaceAsync(text);
            if (!string.IsNullOrEmpty(placeResponse))
            {
                return Content(placeResponse, "application/json");
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
