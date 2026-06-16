using Microsoft.AspNetCore.Authorization;

namespace Pl.Admin.Models.Auth;

public static class PolicyAuthUtils
{
    public static void RegisterAuthorization(AuthorizationOptions options)
    {
        options.AddPolicy(Policies.Admin, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(Roles.Admin)
            )
        );

        options.AddPolicy(Policies.SeniorSupport, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(Roles.Admin, Roles.SeniorSupport)
            )
        );

        options.AddPolicy(Policies.Support, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(
                    Roles.Support, Roles.Admin, Roles.SeniorSupport
                )
            )
        );

        options.AddPolicy(Policies.Developer, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(
                    Roles.Developer, Roles.Admin
                )
            )
        );
    }

    private static bool HasRole(this ClaimsPrincipal user, params string[] roles) =>
        roles.Any(role => user.HasClaim(ClaimTypes.Role, role));
}