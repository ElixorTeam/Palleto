using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.PalletMen;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Validators;

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



