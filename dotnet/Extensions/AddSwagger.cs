using Microsoft.OpenApi.Models;

namespace dotnet.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API",
                Version = "v1",
                Description = "Imersão Fullcycle 14 - Sistema de rastreamento de veículos",
                Contact = new OpenApiContact
                {
                    Name = "Rian Negreiros Dos Santos",
                    Email = "riannegreiros@gmail.com",
                    Url = new Uri("https://github.com/RianNegreiros"),
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

        return app;
    }
}