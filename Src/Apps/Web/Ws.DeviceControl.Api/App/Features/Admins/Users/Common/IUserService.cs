using Ws.DeviceControl.Models.Features.Admins.Users.Commands;
using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.Users.Common;

public interface IUserService :
    IDeleteById,
    IGetById<UserDto>,
    IGetAll<UserDto>
{
    #region Commands

    Task<UserDto> SaveOrUpdateUser(Guid uid, UserUpdateDto updateDto);

    #endregion
}