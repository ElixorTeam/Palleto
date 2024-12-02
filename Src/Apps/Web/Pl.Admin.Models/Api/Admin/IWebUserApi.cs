using Pl.Admin.Models.Features.Admins.Users.Commands;
using Pl.Admin.Models.Features.Admins.Users.Queries;

namespace Pl.Admin.Models.Api.Admin;

public interface IWebUserApi
{
    #region Queries

    [Get("/users/{uid}")]
    Task<UserDto> GetUserByUid(Guid uid);

    [Get("/users")]
    Task<UserDto[]> GetAllUsers();

    #endregion

    #region Commands

    [Delete("/users/{uid}")]
    Task DeleteUser(Guid uid);

    [Post("/users/{uid}")]
    Task<UserDto> SaveOrUpdateUser(Guid uid, [Body] UserUpdateDto updateDto);

    #endregion
}