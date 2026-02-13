using System.Reflection;
using BankingSystem.Application.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using BankingSystem.Application.Adapters;
using BankingSystem.Application.Adapters.Interface;

namespace BankingSystem.Application;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IBankingEventAdapter, BankingEventAdapter>();

        return services;
    }
}