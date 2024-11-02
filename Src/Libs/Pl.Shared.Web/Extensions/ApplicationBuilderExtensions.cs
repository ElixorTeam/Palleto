using Microsoft.AspNetCore.Builder;
using Pl.Shared.Constants;

namespace Pl.Shared.Web.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseApiLocalization(this IApplicationBuilder app)
    {
        CultureInfo.DefaultThreadCurrentCulture = Cultures.Ru;
        CultureInfo.DefaultThreadCurrentUICulture = Cultures.Ru;

        CultureInfo[] supportedCultures = [Cultures.Ru, Cultures.En];
        RequestLocalizationOptions localizationOptions = new()
        {
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures,
            DefaultRequestCulture = new(Cultures.Ru.Name)
        };

        app.UseRequestLocalization(localizationOptions);
    }
}