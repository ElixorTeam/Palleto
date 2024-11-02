using Microsoft.AspNetCore.Authorization;
using Pl.Mobile.Api.App.Features.Users.Common;
using Pl.Mobile.Models.Features.Users;

namespace Pl.Mobile.Api.App.Features.Users;

[AllowAnonymous]
[ApiController]
[Route(ApiEndpoints.Users)]
public sealed class UserController(IUserService userService)
{
    #region Queries

    [HttpGet]
    public UserDto GetUserByCode([FromQuery(Name = "code")] string code) => userService.GetByCode(code);

    #endregion
}