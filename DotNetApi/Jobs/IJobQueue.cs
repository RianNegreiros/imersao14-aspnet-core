using DotNetApi.WebSockets;

namespace DotNetApi.Jobs;
public interface IJobQueue
{
    Task EnqueueAsync(NewPointsPayload job);
    Task<NewPointsPayload> DequeueAsync();
}
