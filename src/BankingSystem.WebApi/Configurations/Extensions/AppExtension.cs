using System.Text.Json.Serialization;
using BankingSystem.Application;
using BankingSystem.Infrastructure;
using BankingSystem.Infrastructure.Context;
using BankingSystem.WebApi.Configurations.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BankingSystem.WebApi.Configurations.Extensions;

public static class AppExtension
{
    public static IServiceCollection AddWebConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var apiInfo = configuration.GetSection("ApiDocumentation");

        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = apiInfo["Title"] ?? "Banking API",
                Version = apiInfo["Version"] ?? "v1",
                Description = apiInfo["Description"],
                Contact = new OpenApiContact
                {
                    Name = apiInfo["ContactName"] ?? "Théo Lucas Nicoleli",
                    Email = apiInfo["ContactEmail"] ?? "theonicoleli@gmail.com"
                }
            });
        });

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }

    public static WebApplication UseWebConfig(this WebApplication app)
    {
        app.UseSwagger();
        
        app.UseSwaggerUI(options =>
        {
            var title = app.Configuration["ApiDocumentation:Title"] ?? "Banking System API";
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
            options.DocumentTitle = title;
        });

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.ApplyMigrations();
        app.MapControllers();

        return app;
    }
    private static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        int retries = 5;
        while (retries > 0)
        {
            try
            {
                if (db.Database.IsRelational() && db.Database.GetPendingMigrations().Any())
                {
                    db.Database.Migrate();
                }
                break;
            }
            catch (Npgsql.NpgsqlException)
            {
                retries--;
                if (retries == 0) throw;
                Console.WriteLine("Aguardando o banco de dados iniciar...");
                Thread.Sleep(5000);
            }
        }
    }
}