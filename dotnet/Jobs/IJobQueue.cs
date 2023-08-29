using dotnet.WebSockets;

namespace dotnet.Jobs;
public interface IJobQueue
{
    Task EnqueueAsync(NewPointsPayload job);
    Task<NewPointsPayload> DequeueAsync();
}
