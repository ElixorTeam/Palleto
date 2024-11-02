using BF.Utilities.Handlers;
using Microsoft.Extensions.Configuration;
using Refit;
using Pl.Mobile.Models;
using Pl.Shared.Web.Extensions;

namespace Pl.Mobile.Client.Source.Shared.Api.Mobile;

internal class MobileRefitClient : IRefitClient
{
    public void Configure(MauiAppBuilder builder)
    {
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Api");

        builder.Services.AddRefitClient<IMobileApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(oidcConfiguration.GetValueSafe<string>("BaseUrl")))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .AddHttpMessageHandler<AcceptLanguageHandler>();
    }
}