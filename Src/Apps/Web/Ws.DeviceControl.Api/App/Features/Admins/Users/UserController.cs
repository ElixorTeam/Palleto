using Ws.DeviceControl.Api.App.Features.Admins.Users.Common;
using Ws.DeviceControl.Models.Features.Admins.Users.Commands;
using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.Users;

[ApiController]
[Route(ApiEndpoints.Users)]
[Authorize(PolicyEnum.SeniorSupport)]
public sealed class UserController(IUserService userService)
{
    #region Queries

    [HttpGet]
    public Task<UserDto[]> GetAllUsers() =>
        userService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<UserDto> GetById([FromRoute] Guid id) =>
        userService.GetByIdAsync(id);

    #endregion

    #region Commands

    [HttpPost("{id:guid}")]
    public Task<UserDto> SaveOrUpdate([FromRoute] Guid id, [FromBody] UserUpdateDto dto) =>
        userService.SaveOrUpdateUser(id, dto);

    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) =>
        userService.DeleteAsync(id);

    #endregion
}