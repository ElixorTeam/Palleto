using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Auth.Handlers;

public class ServerAuthorizationMessageHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpContext? context = httpContextAccessor.HttpContext;
        if (context == null)
            return await base.SendAsync(request, cancellationToken);

        AuthenticateResult authenticateResult = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (authenticateResult.Properties == null)
            return await base.SendAsync(request, cancellationToken);

        string? accessToken = authenticateResult.Properties.GetTokenValue("access_token");
        request.Headers.Authorization = new("Bearer", accessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}