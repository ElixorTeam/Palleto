using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.Admins.PalletMen.Impl.Expressions;
using Pl.Admin.Api.App.Features.Admins.PalletMen.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Ref.PalletMen;
using Pl.Admin.Models.Features.Admins.PalletMen.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.Admins.PalletMen.Impl.Validators;

public class PalletManCreateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiCreateValidator<PalletManEntity, PalletManCreateDto>
{
    public override async Task ValidateAsync(DbSet<PalletManEntity> dbSet, PalletManCreateDto dto)
    {
        Fio fio = new(dto.Surname, dto.Name, dto.Patronymic);
        UqPalletManProperties uqProperties = new(dto.Id1C, fio, dto.Password);

        await ValidateProperties(new PalletManCreateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, PalletManExpressions.GetUqPredicates(uqProperties));
    }
}



