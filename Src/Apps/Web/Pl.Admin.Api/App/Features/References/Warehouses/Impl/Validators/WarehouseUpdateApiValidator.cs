using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.References.Warehouses.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.Warehouses.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Ref.Warehouses;
using Pl.Admin.Models.Features.References.Warehouses.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.References.Warehouses.Impl.Validators;

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