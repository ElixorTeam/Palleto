﻿using System.Security.Claims;
using DeviceControl.Auth.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DeviceControl.Auth;

public static class PolicyAuthUtils
{
    public static void RegisterAuthorization(AuthorizationOptions options)
    {
        options.FallbackPolicy = options.DefaultPolicy;
        options.AddPolicy(PolicyNameUtils.Admin, builder =>
            builder.RequireRole(ClaimTypes.Role, RolesNameUtils.Admin)
        );
        
        options.AddPolicy(PolicyNameUtils.Support, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(
                    RolesNameUtils.Sup, RolesNameUtils.Sup13, 
                    RolesNameUtils.Sup16, RolesNameUtils.SupKamni
                )
            )
        );
    }

    private static bool HasRole(this ClaimsPrincipal user, params string[] roles)
    {
        return roles.Any(role => user.HasClaim(ClaimTypes.Role, role));
    }
}