using Pl.Mobile.Models.Features.Users;

namespace Pl.Mobile.Api.App.Features.Users.Common;

public interface IUserService
{
    #region Queries

    UserDto GetByCode(string code);

    #endregion
}