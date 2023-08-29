using dotnet.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.WebSockets;

[ApiController]
[Route("socket.io/")]
public class SocketController : ControllerBase
{
    private readonly RoutesGateway _routesGateway;
    private readonly IJobQueue _jobProcessorService;

    public SocketController(RoutesGateway routesGateway, IJobQueue jobProcessorService)
    {
        _routesGateway = routesGateway;
        _jobProcessorService = jobProcessorService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Socket.IO server is running.");
    }

    [HttpPost]
    public IActionResult SendNewPoints([FromBody] NewPointsPayload payload)
    {
        _routesGateway.SendMessage(payload.RouteId, payload.Lat, payload.Lng);
        _jobProcessorService.EnqueueAsync(payload);
        return Ok("Message sent to Socket.IO server.");
    }
}
