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

    [HttpGet("{id}")]
    public async Task<IActionResult> FindOne([FromRoute] string id)
    {
        var route = await _routesService.FindOneAsync(id);
        if (route == null)
        {
            return NotFound();
        }

        return Ok(route);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRoute([FromQuery] string id, [FromBody] UpdateRouteDto routeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedRoute = await _routesService.UpdateAsync(id, routeDto);
        if (updatedRoute == null)
        {
            return NotFound();
        }

        return Ok(updatedRoute);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoute([FromQuery] string id)
    {
        var deletedRoute = await _routesService.RemoveAsync(id);

        if (deletedRoute)
        {
            return NotFound();
        }

        return Ok(deletedRoute);
    }
}
