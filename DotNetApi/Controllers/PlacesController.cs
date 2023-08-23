using DotNetApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers;
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
            var place = await _placesService.FindPlaceAsync(text);
            return Ok(place);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
