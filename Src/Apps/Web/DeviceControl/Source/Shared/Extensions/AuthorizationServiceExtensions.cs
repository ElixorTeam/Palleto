using System.Security.Claims;

namespace DeviceControl.Source.Shared.Extensions;

public static class AuthorizationServiceExtensions
{
    public static bool ValidatePolicy(this IAuthorizationService authorizationService, ClaimsPrincipal user, string policyName) =>
        authorizationService.AuthorizeAsync(user, policyName).GetAwaiter().GetResult().Succeeded;
}