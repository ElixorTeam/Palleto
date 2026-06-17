using BlazorBlueprint.Primitives.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Pl.Components;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPlComponents(this IServiceCollection services)
    {
        // Register all primitive services (portal, focus, positioning, dropdown manager, keyboard shortcuts)
        services.AddBlazorBlueprintPrimitives();
        services.AddScoped<ToastService>();
        services.AddScoped<DialogService>();
        return services;
    }
}