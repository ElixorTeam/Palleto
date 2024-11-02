using Pl.Tablet.Models.Features.Users;

namespace Pl.Tablet.Api.App.Features.Users.Common;

public interface IUserService
{
    #region Queries

    UserDto GetByCode(string code);

    #endregion
}