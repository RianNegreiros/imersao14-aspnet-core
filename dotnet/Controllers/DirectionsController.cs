using dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DirectionsController : ControllerBase
{
    private readonly GoogleMapsService _googleMapsService;

    public DirectionsController(GoogleMapsService googleMapsService)
    {
        _googleMapsService = googleMapsService;
    }

    [SwaggerOperation(Summary = "Obtém as direções entre origem e destino")]
    [SwaggerResponse(200, "Direções obtidas com sucesso")]
    [SwaggerResponse(404, "Direções não encontradas")]
    [SwaggerResponse(500, "Erro interno do servidor")]
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