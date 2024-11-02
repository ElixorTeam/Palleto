using Refit;

namespace Pl.Admin.Client.Source.Shared.Api.Labelary;

internal class LabelaryRefitClient : IRefitClient
{
    public void Configure(WebApplicationBuilder builder)
    {
        string apiUrl = builder.Configuration.GetValue<string>("LabelaryApi")!;
        builder.Services.AddRefitClient<ILabelaryApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(apiUrl));
    }
}