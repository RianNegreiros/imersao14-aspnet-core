using dotnet.WebSockets;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace dotnet.Jobs;
public class RedisJobQueue : IJobQueue
{
    private readonly IDatabase _database;

    public RedisJobQueue(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }

    public async Task EnqueueAsync(NewPointsPayload job)
    {
        var json = JsonConvert.SerializeObject(job);
        await _database.ListLeftPushAsync("jobQueue", json);
    }

    public async Task<NewPointsPayload> DequeueAsync()
    {
        var json = await _database.ListRightPopAsync("jobQueue");
        if (json == RedisValue.Null)
            return null;

        return JsonConvert.DeserializeObject<NewPointsPayload>(json);
    }
}
