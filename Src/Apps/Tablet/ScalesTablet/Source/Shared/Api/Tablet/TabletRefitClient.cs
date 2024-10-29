using BF.Utilities.Handlers;
using Microsoft.Extensions.Configuration;
using Refit;
using Ws.Shared.Web.Extensions;
using Ws.Tablet.Models;

namespace ScalesTablet.Source.Shared.Api.Tablet;

internal class TabletRefitClient : IRefitClient
{
    public void Configure(MauiAppBuilder builder)
    {
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Api");

        builder.Services.AddRefitClient<ITabletApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(oidcConfiguration.GetValueSafe<string>("BaseUrl")))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .AddHttpMessageHandler<AndroidIdMessageHandler>()
            .AddHttpMessageHandler<AcceptLanguageHandler>();
    }
}