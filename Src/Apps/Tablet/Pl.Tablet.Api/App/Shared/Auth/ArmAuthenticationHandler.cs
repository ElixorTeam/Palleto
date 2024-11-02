using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Pl.Tablet.Api.App.Shared.Auth;

public class ArmAuthenticationHandler(
    IOptionsMonitor<ArmAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
    ) : AuthenticationHandler<ArmAuthenticationOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(Options.TokenHeaderName, out StringValues authHeader))
            return await Task.FromResult(AuthenticateResult.Fail($"Missing header: {Options.TokenHeaderName}"));

        string[] headerValue = authHeader.ToString().Split(' ', 2);

        if (headerValue is not [ArmAuthenticationOptions.DefaultScheme, _])
            return AuthenticateResult.Fail("Invalid Authorization header format or scheme. " +
                                           $"Expected: {ArmAuthenticationOptions.DefaultScheme}");

        string token = headerValue[1];

        // Arm? arm = await dbContext.Lines
        //     .Where(i => i.PcName == token)
        //     .Select(i => new Arm(i.Id, i.Type, i.Warehouse.Id))
        //     .FirstOrDefaultAsync();
        //
        // if (arm == null)
        //     return AuthenticateResult.Fail($"Invalid value in header: {Options.TokenHeaderName}");

        List<Claim> claims = [
            new(ClaimTypes.NameIdentifier, token),
            new(ClaimTypes.StreetAddress, Guid.NewGuid().ToString())
        ];

        ClaimsIdentity claimsIdentity = new(claims, Scheme.Name);
        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

        return await Task.FromResult(AuthenticateResult.Success(new(claimsPrincipal, Scheme.Name)));
    }
}