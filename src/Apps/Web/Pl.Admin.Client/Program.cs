using BF.Utilities.Handlers;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Fluxor;
using Pl.Admin.Client;
using Pl.Admin.Client.Source.App;
using Pl.Admin.Client.Source.Shared.Api;
using Pl.Admin.Client.Source.Shared.Auth;
using Pl.Admin.Client.Source.Shared.Auth.Options;
using Pl.Admin.Models;
using Pl.Components;
using Pl.Shared.Constants;
using Pl.Shared.Web.Extensions;
using TailwindMerge.Extensions;
using Routes = Pl.Admin.Client.Source.Shared.Constants.Routes;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

OidcOptions oidcOptions = builder.Configuration
    .GetRequiredSection("Oidc").Get<OidcOptions>() ?? throw new NullReferenceException();

builder.RegisterRefitClients();

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => options.DetailedErrors = true);

builder.Services
    .AddPlComponents()
    .AddUserClaims()
    .AddHelpers<IAdminAssembly>()
    .AddRefitEndpoints<IAdminAssembly>()
    .AddDelegatingHandlers<IAdminAssembly>()
    .AddValidators<IAdminModelsAssembly>()
    .AddTransient<AcceptLanguageHandler>();

builder.Services
    .AddBlazorise()
    .AddEmptyProviders()
    .AddFontAwesomeIcons()
    .AddWMBOS()
    .AddLocalization()
    .AddFluxor(c => c.ScanAssemblies(typeof(IAdminAssembly).Assembly))
    .AddFluentUIComponents(c => c.ValidateClassNames = false)
    .ConfigureKeycloakAuthorization(oidcOptions)
    .AddTailwindMerge();

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
    .MapGroup(Routes.Authorization)
    .MapLoginAndLogout(oidcOptions.Scheme);

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();