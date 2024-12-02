using Pl.Admin.Client.Source.Shared.Auth.Settings;
using Pl.Admin.Client.Source.Shared.Auth.Shared;
using Pl.Admin.Client.Source.Shared.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Pl.Admin.Client.Source.Shared.Auth;

internal static class AuthSetupServiceCollectionExtensions
{
    internal static IServiceCollection ConfigureKeycloakAuthorization(this IServiceCollection services, OidcSettings oidcConfiguration)
    {
        services
            .AddAuthentication(oidcConfiguration.Scheme)
            .ConfigureCookieAuthentication()
            .ConfigureOpenIdConnectAuthentication(oidcConfiguration);

        services
            .AddHttpContextAccessor()
            .AddAuthorization(PolicyAuthUtils.RegisterAuthorization)
            .ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme, oidcConfiguration.Scheme);

        services.AddScoped<AuthenticationStateProvider, CustomRevalidatingAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();

        return services;
    }
    private static IServiceCollection ConfigureCookieOidcRefresh(this IServiceCollection services, string cookieScheme, string oidcScheme)
    {
        services.AddSingleton<CookieOidcRefresher>();

        services.AddOptions<CookieAuthenticationOptions>(cookieScheme)
            .Configure<CookieOidcRefresher>(
                (cookieOptions, refresher) =>
                    cookieOptions.Events.OnValidatePrincipal = context => refresher.ValidateOrRefreshCookieAsync(context, oidcScheme)
            );

        services.AddOptions<OpenIdConnectOptions>(oidcScheme)
            .Configure(oidcOptions => {
                oidcOptions.Scope.Add(OpenIdConnectScope.OfflineAccess);
                oidcOptions.SaveTokens = true;
            });

        return services;
    }
}

#region File scope

file static class AuthBuilderExtensions
{
    public static AuthenticationBuilder ConfigureCookieAuthentication(this AuthenticationBuilder authBuilder)
    {
        authBuilder
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromDays(30);
                opt.SlidingExpiration = true;

                opt.Events.OnSigningIn = context =>
                {
                    context.Properties.IsPersistent = true;
                    context.Properties.ExpiresUtc = DateTimeOffset.UtcNow.Add(opt.ExpireTimeSpan);
                    return Task.CompletedTask;
                };

                opt.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.Redirect(Urls.Home);
                    return Task.CompletedTask;
                };
            });

        return authBuilder;
    }

    public static AuthenticationBuilder ConfigureOpenIdConnectAuthentication(this AuthenticationBuilder authBuilder, OidcSettings oidcConfiguration)
    {
        authBuilder
            .AddOpenIdConnect(oidcConfiguration.Scheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Authority = oidcConfiguration.AuthorityFull;
                options.ClientId = oidcConfiguration.ClientId;
                options.ClientSecret = oidcConfiguration.ClientSecret;
                options.RequireHttpsMetadata = oidcConfiguration.RequireHttpsMetadata;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.MapInboundClaims = false;

                options.Events = new()
                {
                    OnUserInformationReceived = context =>
                    {
                        ClaimsIdentity? claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
                        string clientId = context.Options.ClientId ?? string.Empty;
                        Dictionary<string, string> claimsDict = context.User.RootElement.EnumerateObject()
                            .ToDictionary(claim => claim.Name, claim => claim.Value.ToString());
                        if (string.IsNullOrWhiteSpace(clientId) || claimsIdentity == null) return Task.CompletedTask;

                        ClaimsMapping.MapJwtClaims(claimsDict, claimsIdentity, clientId);
                        return Task.CompletedTask;
                    }
                };
            });
        return authBuilder;
    }
}

#endregion