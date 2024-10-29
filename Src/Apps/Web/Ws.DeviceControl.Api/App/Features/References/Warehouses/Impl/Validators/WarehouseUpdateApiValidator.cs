using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.Warehouses;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Validators;

public class WarehouseUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiUpdateValidator<WarehouseEntity, WarehouseUpdateDto, Guid>
{
    public override async Task<WarehouseEntity> ValidateAndGetAsync(DbSet<WarehouseEntity> dbSet, WarehouseUpdateDto dto, Guid id)
    {
        UqWarehousesProperties uqProperties = new(dto.Name, dto.Id1C);

        await ValidateProperties(new WarehouseUpdateValidator(wsDataLocalizer), dto);
        return await ValidatePredicatesAsync(dbSet, WarehouseExpressions.GetUqPredicates(uqProperties), i => i.Id == id);
    }
}