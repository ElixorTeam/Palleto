using Ws.Database.Entities.Ref.PalletMen;
using Ws.Database.Entities.Ref.Warehouses;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Extensions;

internal static class PalletManDtoExtensions
{
    public static PalletManEntity ToEntity(this PalletManCreateDto dto, WarehouseEntity warehouse)
    {
        return new()
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Patronymic = dto.Patronymic,
            Password = dto.Password,
            Uid1C = dto.Id1C,
            Warehouse = warehouse
        };
    }

    public static void UpdateEntity(this PalletManUpdateDto dto, PalletManEntity entity, WarehouseEntity warehouse)
    {
        entity.Name = dto.Name;
        entity.Patronymic = dto.Patronymic;
        entity.Surname = dto.Surname;
        entity.Warehouse = warehouse;
        entity.Uid1C = dto.Id1C;
    }
}