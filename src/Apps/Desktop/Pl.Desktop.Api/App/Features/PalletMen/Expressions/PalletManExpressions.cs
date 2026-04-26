using Pl.Database.Entities.Ref.PalletMen;
using Pl.Desktop.Models.Features.PalletMen;

namespace Pl.Desktop.Api.App.Features.PalletMen.Expressions;

internal static class PalletManExpressions
{
    public static Expression<Func<PalletManEntity, PalletManDto>> ToDto => palletMan =>
        new()
        {
            Id = palletMan.Id,
            Fio = new(palletMan.Surname, palletMan.Name, palletMan.Patronymic),
            Password = palletMan.Password
        };
}