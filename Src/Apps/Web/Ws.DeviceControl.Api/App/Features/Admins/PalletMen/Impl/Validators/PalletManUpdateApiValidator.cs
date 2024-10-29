using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.PalletMen;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Validators;

public class PalletManUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiUpdateValidator<PalletManEntity, PalletManUpdateDto, Guid>
{
    public override async Task<PalletManEntity> ValidateAndGetAsync(DbSet<PalletManEntity> dbSet, PalletManUpdateDto dto, Guid id)
    {
        Fio fio = new(dto.Surname, dto.Name, dto.Patronymic);

        UqPalletManProperties uqProperties = new(dto.Id1C, fio, dto.Password);
        await ValidateProperties(new PalletManUpdateValidator(wsDataLocalizer), dto);

        return await ValidatePredicatesAsync(dbSet, PalletManExpressions.GetUqPredicates(uqProperties), i => i.Id == id);
    }
}