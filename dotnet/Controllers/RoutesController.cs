using dotnet.DTOs;
using dotnet.Models;
using dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace dotnet.Controllers;

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
    [SwaggerOperation(Summary = "Cria uma nova rota")]
    [ProducesResponseType(typeof(RouteModel), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [Consumes("application/json")]
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
    [SwaggerOperation(Summary = "Obtém todas as rotas")]
    [ProducesResponseType(typeof(List<RouteModel>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> FindAll()
    {
        var routes = await _routesService.FindAllAsync();
        return Ok(routes);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtém uma rota pelo id")]
    [ProducesResponseType(typeof(RouteModel), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [Consumes("application/json")]
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
    [SwaggerOperation(Summary = "Atualiza uma rota pelo id")]
    [ProducesResponseType(typeof(RouteModel), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
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
    [SwaggerOperation(Summary = "Remove uma rota pelo id")]
    [ProducesResponseType(typeof(RouteModel), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [Consumes("application/json")]
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
