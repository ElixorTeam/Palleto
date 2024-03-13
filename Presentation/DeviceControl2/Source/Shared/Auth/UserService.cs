using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace DeviceControl2.Source.Shared.Auth;

public class UserService(AuthenticationStateProvider authenticationStateProvider)
{
    public async Task<ClaimsPrincipal?> GetUser()
    {
        AuthenticationState authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        return authState.User;
    }
}