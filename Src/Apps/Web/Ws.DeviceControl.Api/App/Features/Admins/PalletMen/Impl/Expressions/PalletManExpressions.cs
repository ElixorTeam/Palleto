using Ws.Database.Entities.Ref.PalletMen;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Expressions;

public static class PalletManExpressions
{
    public static Expression<Func<PalletManEntity, PalletManDto>> ToDto =>
        palletMan => new()
        {
            Id = palletMan.Id,
            Id1C = palletMan.Uid1C,
            Fio = new(palletMan.Surname, palletMan.Name, palletMan.Patronymic),
            Password = palletMan.Password,
            Warehouse = new(palletMan.Warehouse.Id, palletMan.Warehouse.Name),
            ProductionSite = new(palletMan.Warehouse.ProductionSite.Id, palletMan.Warehouse.ProductionSite.Name),
            CreateDt = palletMan.CreateDt,
            ChangeDt = palletMan.ChangeDt
        };

    public static List<PredicateField<PalletManEntity>> GetUqPredicates(UqPalletManProperties uqManProperties) =>
    [
        new(i =>
            i.Name == uqManProperties.Fio.Name &&
            i.Surname == uqManProperties.Fio.Surname &&
            i.Patronymic == uqManProperties.Fio.Patronymic,
        "FIO"),

        new(i => i.Uid1C == uqManProperties.Id1C, "Uid1C"),
        new(i => i.Password == uqManProperties.Password, "Password"),
    ];
}