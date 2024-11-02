using Pl.Mobile.Api.App.Features.Users.Common;
using Pl.Mobile.Models.Features.Users;

namespace Pl.Mobile.Api.App.Features.Users.Impl;

internal sealed class UserApiService : IUserService
{
    #region Queries

    public UserDto GetByCode(string code)
    {
        Dictionary<string, UserDto> users = new()
        {
            { "1234", new()
                {
                    Id = Guid.NewGuid(),
                    Fio = new("Александров", "Даниил", "Дмитриевич"),
                    WarehouseName = "Склад № 2"
                }
            },
            { "4321", new()
                {
                    Id = Guid.NewGuid(),
                    Fio = new("Власов","Артем", "Алексеевич"),
                    WarehouseName = "Склад № 3"
                }
            },
        };
        return users.TryGetValue(code, out UserDto? user) ? user : throw new ApiInternalException
        {
            ErrorDisplayMessage = "Пользователь не найден",
            StatusCode = HttpStatusCode.NotFound
        };
    }

    #endregion
}