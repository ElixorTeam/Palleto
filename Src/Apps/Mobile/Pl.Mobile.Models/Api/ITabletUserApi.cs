using Pl.Mobile.Models.Features.Users;

namespace Pl.Mobile.Models.Api;

public interface ITabletUserApi
{
    [Get("/users")]
    Task<UserDto> GetUserByCode(ushort code);
}