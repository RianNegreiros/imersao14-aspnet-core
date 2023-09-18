using dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Obtém os locais próximos a uma coordenada")]
    [SwaggerResponse(200, "Locais obtidos com sucesso")]
    [SwaggerResponse(404, "Locais não encontrados")]
    [SwaggerResponse(500, "Erro interno do servidor")]
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
