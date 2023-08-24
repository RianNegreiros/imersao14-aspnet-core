using DotNetApi.Data;
using DotNetApi.Jobs;
using DotNetApi.Services;
using DotNetApi.WebSockets;
using Prometheus;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddScoped<RoutesService>();
builder.Services.AddHttpClient("GoogleMapsClient", client =>
{
    client.BaseAddress = new Uri("https://maps.googleapis.com");
});
builder.Services.AddScoped<GoogleMapsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddSingleton(new RoutesGateway("http://localhost:5000"));

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("redis:6379"));
builder.Services.AddSingleton<IJobQueue, RedisJobQueue>();
builder.Services.AddSingleton<KafkaProducerService>();
builder.Services.AddHostedService<JobProcessorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMetricServer();

app.MapControllers();

app.Run();
