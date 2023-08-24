using DotNetApi.DTOs;
using DotNetApi.Services;
using DotNetApi.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RoutesController : ControllerBase
{
    private readonly RoutesService _routesService;
    private readonly RoutesGateway _routesGateway;
    public RoutesController(RoutesService routesService, RoutesGateway routesGateway)
    {
        _routesService = routesService;
        _routesGateway = routesGateway;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoute([FromBody] CreateRouteDto routeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdRoute = await _routesService.CreateRouteAsync(routeDto);
        return Ok(createdRoute);
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var routes = await _routesService.FindAllAsync();
        return Ok(routes);
    }

    [HttpPost("send-new-points")]
    public IActionResult SendNewPoints([FromBody] NewPointsPayload payload)
    {
        _routesGateway.SendMessage(payload.RouteId, payload.Lat, payload.Lng);
        return Ok("Message sent to Socket.IO server.");
    }
}
