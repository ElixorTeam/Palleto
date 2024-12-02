using Pl.Database.Entities.Ref.Users;
using Pl.Admin.Models.Features.Admins.Users.Queries;

namespace Pl.Admin.Api.App.Features.Admins.Users.Impl.Expressions;

public static class UserExpressions
{
    public static Expression<Func<UserEntity, UserDto>> ToDto =>
        user => new()
        {
            Id = user.Id,
            ProductionSite = ProxyUtils.ProductionSite(user.ProductionSite)
        };
}