using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Pl.Admin.Client.Source.Shared.Auth;

internal static class LoginLogoutEndpointRouteBuilderExtensions
{
    internal static IEndpointConventionBuilder MapLoginAndLogout(this IEndpointRouteBuilder endpoints, string oidcScheme)
    {
        RouteGroupBuilder group = endpoints.MapGroup(string.Empty);

        group.MapGet("/login", (string? returnUrl) => TypedResults.Challenge(GetAuthProperties(returnUrl)))
            .AllowAnonymous();

        group.MapPost("/logout", ([FromForm] string? returnUrl) => TypedResults.SignOut(GetAuthProperties(returnUrl),
            [CookieAuthenticationDefaults.AuthenticationScheme, oidcScheme]));

        return group;
    }

    private static AuthenticationProperties GetAuthProperties(string? returnUrl)
    {
        const string pathBase = "/";
        string redirectUri = string.Empty;

        if (string.IsNullOrWhiteSpace(returnUrl))
            redirectUri = pathBase;
        else if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
            redirectUri = new Uri(returnUrl, UriKind.Absolute).PathAndQuery;
        else if (returnUrl[0] != '/')
            redirectUri = $"{pathBase}{returnUrl}";

        return new() { RedirectUri = redirectUri };
    }
}