using BF.Utilities.Handlers;
using Microsoft.Extensions.Configuration;
using Pl.Desktop.Client.Source.Shared.Api.Desktop.Handlers;

namespace Pl.Desktop.Client.Source.Shared.Api.Desktop;

internal class DesktopRefitClient : IRefitClient
{
    public void Configure(MauiAppBuilder builder)
    {
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Api");
        builder.Services.AddRefitClient<IDesktopApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{oidcConfiguration.GetValue<string>("BaseUrl") ?? ""}"))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .AddHttpMessageHandler<HostNameMessageHandler>()
            .AddHttpMessageHandler<AcceptLanguageHandler>();
    }
}