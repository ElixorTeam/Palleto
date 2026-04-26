using Pl.Admin.Models.Features.Admins.Users.Commands;
using Pl.Admin.Models.Features.Admins.Users.Queries;

namespace Pl.Admin.Api.App.Features.Admins.Users.Common;

public interface IUserService :
    IDeleteById,
    IGetById<UserDto>,
    IGetAll<UserDto>
{
    #region Commands

    Task<UserDto> SaveOrUpdateUser(Guid uid, UserUpdateDto updateDto);

    #endregion
}