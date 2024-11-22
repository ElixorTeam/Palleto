using BF.Utilities.Handlers;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Fluxor;
using Pl.Admin.Client;
using Pl.Admin.Client.Source.App;
using Pl.Admin.Client.Source.Shared.Api;
using Pl.Admin.Client.Source.Shared.Auth;
using Pl.Admin.Client.Source.Shared.Auth.Settings;
using Pl.Admin.Client.Source.Shared.Constants;
using Pl.Admin.Models;
using Pl.Shared.Constants;
using Pl.Shared.Web.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

OidcSettings oidcSettings = builder.Configuration
    .GetSection("Oidc").Get<OidcSettings>() ?? throw new NullReferenceException();

builder.RegisterRefitClients();

builder.Services
    .AddUserClaims()
    .AddHelpers<IAdminAssembly>()
    .AddRefitEndpoints<IAdminAssembly>()
    .AddDelegatingHandlers<IAdminAssembly>()
    .AddValidators<IAdminModelsAssembly>()
    .AddTransient<AcceptLanguageHandler>();

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => options.DetailedErrors = true);

builder.Services
    .AddBlazorise()
    .AddEmptyProviders()
    .AddFontAwesomeIcons()
    .AddWMBOS()
    .AddLocalization()
    .AddFluxor(c => c.ScanAssemblies(typeof(IAdminAssembly).Assembly))
    .AddFluentUIComponents(c => c.ValidateClassNames = false)
    .ConfigureKeycloakAuthorization(oidcSettings);

// builder.Services.AddRazorPages();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseRequestLocalization(Cultures.Ru.Name);

app.UseStatusCodePagesWithRedirects("/not-found");

app
    .MapGroup(Urls.Authorization)
    .MapLoginAndLogout(oidcSettings.Scheme);

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();