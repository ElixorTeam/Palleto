using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.Lines;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Validators;

public class ArmCreateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiCreateValidator<LineEntity, ArmCreateDto>
{
    public override async Task ValidateAsync(DbSet<LineEntity> dbSet, ArmCreateDto dto)
    {
        UqArmProperties uqProperties = new(dto.SystemKey, dto.Name, dto.Number);
        await ValidateProperties(new ArmCreateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, ArmExpressions.GetUqPredicates(uqProperties));
    }
}



