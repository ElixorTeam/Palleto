using BF.Utilities.Handlers;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using DeviceControl;
using DeviceControl.Source.App;
using DeviceControl.Source.Shared.Api;
using DeviceControl.Source.Shared.Auth;
using DeviceControl.Source.Shared.Auth.Settings;
using DeviceControl.Source.Shared.Constants;
using Fluxor;
using Ws.Shared.Web.Extensions;
using Ws.Shared.Constants;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

OidcSettings oidcSettings = builder.Configuration
    .GetSection("Oidc").Get<OidcSettings>() ?? throw new NullReferenceException();

builder.RegisterRefitClients();

builder.Services
    .AddUserClaims()
    .AddHelpers<IDeviceControlAssembly>()
    .AddRefitEndpoints<IDeviceControlAssembly>()
    .AddDelegatingHandlers<IDeviceControlAssembly>()
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
    .AddFluxor(c => c.ScanAssemblies(typeof(IDeviceControlAssembly).Assembly))
    .AddFluentUIComponents(c => c.ValidateClassNames = false)
    .ConfigureKeycloakAuthorization(oidcSettings);

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

app
    .MapGroup(Urls.Authorization)
    .MapLoginAndLogout(oidcSettings.Scheme);

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();