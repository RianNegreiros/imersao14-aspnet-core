using DotNetApi.DTOs;
using DotNetApi.WebSockets;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DotNetApi.Services;
public class JobProcessorService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly IConnectionMultiplexer _redisConnection;

    public JobProcessorService(IServiceProvider provider, IConnectionMultiplexer redisConnection)
    {
        _provider = provider;
        _redisConnection = redisConnection;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var database = _redisConnection.GetDatabase();
        var queueName = "jobQueue";

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var json = await database.ListRightPopAsync(queueName);

                if (json.HasValue)
                {
                    var jobData = JsonConvert.DeserializeObject<NewPointsPayload>(json);
                    await ProcessJob(jobData);
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private async Task ProcessJob(NewPointsPayload job)
    {
        using (var scope = _provider.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var routesDriverService = serviceProvider.GetRequiredService<RoutesDriverService>();

            var newRoutesDriver = new RoutesDriverDto
            {
                RouteId = job.RouteId,
                Lat = job.Lat,
                Lng = job.Lng
            };
            await routesDriverService.CreateOrUpdateAsync(newRoutesDriver);
        }
    }
}
