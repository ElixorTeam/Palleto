using Pl.Tablet.Models.Features.Users;

namespace Pl.Tablet.Models.Api;

public interface ITabletUserApi
{
    [Get("/users")]
    Task<UserDto> GetUserByCode(ushort code);
}