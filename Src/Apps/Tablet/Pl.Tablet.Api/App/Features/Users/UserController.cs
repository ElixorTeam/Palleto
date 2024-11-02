using Pl.Tablet.Api.App.Features.Users.Common;
using Pl.Tablet.Models.Features.Users;

namespace Pl.Tablet.Api.App.Features.Users;

[ApiController]
[Route(ApiEndpoints.Users)]
public sealed class UserController(IUserService userService)
{
    #region Queries

    [HttpGet]
    public UserDto GetUserByCode([FromQuery(Name = "code")] string code) => userService.GetByCode(code);

    #endregion
}