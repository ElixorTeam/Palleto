using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Pl.Database;
using Pl.Shared.Enums;

namespace Pl.Desktop.Api.App.Shared.Auth;

file record Arm(Guid Id, ArmType Role, Guid WarehouseId);

public class ArmAuthenticationHandler(
    IOptionsMonitor<ArmAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    WsDbContext dbContext
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

        if (!Guid.TryParse(headerValue[1], out Guid systemKey))
            return AuthenticateResult.Fail($"Invalid value in header: {Options.TokenHeaderName}");

        Arm? arm = await dbContext.Lines
            .Where(i => i.SystemKey == systemKey)
            .Select(i => new Arm(i.Id, i.Type, i.Warehouse.Id))
            .FirstOrDefaultAsync();

        if (arm == null)
            return AuthenticateResult.Fail($"Invalid value in header: {Options.TokenHeaderName}");

        List<Claim> claims = [
            new(ClaimTypes.NameIdentifier, arm.Id.ToString()),
            new(ClaimTypes.Role, arm.Role.ToString()),
            new(ClaimTypes.StreetAddress, arm.WarehouseId.ToString())
        ];

        ClaimsIdentity claimsIdentity = new(claims, Scheme.Name);
        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

        return await Task.FromResult(AuthenticateResult.Success(new(claimsPrincipal, Scheme.Name)));
    }
}