using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.Lines;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Validators;

public class ArmUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiUpdateValidator<LineEntity, ArmUpdateDto, Guid>
{
    public override async Task<LineEntity> ValidateAndGetAsync(DbSet<LineEntity> dbSet, ArmUpdateDto dto, Guid id)
    {
        UqArmProperties uqProperties = new(dto.SystemKey, dto.Name, dto.Number);
        await ValidateProperties(new ArmUpdateValidator(wsDataLocalizer), dto);
        return await ValidatePredicatesAsync(dbSet, ArmExpressions.GetUqPredicates(uqProperties), i => i.Id == id);
    }
}