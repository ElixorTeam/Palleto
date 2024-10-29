using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.Warehouses;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Validators;

public class WarehouseCreateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiCreateValidator<WarehouseEntity, WarehouseCreateDto>
{
    public override async Task ValidateAsync(DbSet<WarehouseEntity> dbSet, WarehouseCreateDto dto)
    {
        UqWarehousesProperties uqProperties = new(dto.Name, dto.Id1C);
        await ValidateProperties(new WarehouseCreateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, WarehouseExpressions.GetUqPredicates(uqProperties));
    }
}



