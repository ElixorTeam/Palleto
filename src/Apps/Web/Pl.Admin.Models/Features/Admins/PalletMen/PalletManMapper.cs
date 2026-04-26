using Pl.Admin.Models.Features.Admins.PalletMen.Commands;
using Pl.Admin.Models.Features.Admins.PalletMen.Queries;

namespace Pl.Admin.Models.Features.Admins.PalletMen;

public static class PalletManMapper
{
    public static PalletManUpdateDto DtoToUpdateDto(PalletManDto item)
    {
        return new()
        {
            Id1C = item.Id1C,
            Name = item.Fio.Name,
            Surname = item.Fio.Surname,
            Patronymic = item.Fio.Patronymic,
            Password = item.Password,
            WarehouseId = item.Warehouse.Id
        };
    }
}