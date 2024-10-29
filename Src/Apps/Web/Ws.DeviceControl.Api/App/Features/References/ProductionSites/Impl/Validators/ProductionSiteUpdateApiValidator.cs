using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Validators;

public class ProductionSiteUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiUpdateValidator<ProductionSiteEntity, ProductionSiteUpdateDto, Guid>
{
    public override async Task<ProductionSiteEntity> ValidateAndGetAsync(DbSet<ProductionSiteEntity> dbSet, ProductionSiteUpdateDto dto, Guid id)
    {
        UqProductionSiteProperties uqProperties = new(dto.Name);

        await ValidateProperties(new ProductionSiteUpdateValidator(wsDataLocalizer), dto);
        return await ValidatePredicatesAsync(dbSet, ProductionSiteExpressions.GetUqPredicates(uqProperties), i => i.Id == id);
    }
}