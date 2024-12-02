using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.References.Warehouses.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.Warehouses.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Ref.Warehouses;
using Pl.Admin.Models.Features.References.Warehouses.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.References.Warehouses.Impl.Validators;

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



