using DotNetApi.DTOs;
using DotNetApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RoutesController : ControllerBase
{
    private readonly RoutesService _routesService;
    public RoutesController(RoutesService routesService)
    {
        _routesService = routesService;
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
}
