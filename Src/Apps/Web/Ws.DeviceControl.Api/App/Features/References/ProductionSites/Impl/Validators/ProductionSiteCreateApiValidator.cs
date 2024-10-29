using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Validators;

public class ProductionSiteCreateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiCreateValidator<ProductionSiteEntity, ProductionSiteCreateDto>
{
    public override async Task ValidateAsync(DbSet<ProductionSiteEntity> dbSet, ProductionSiteCreateDto dto)
    {
        UqProductionSiteProperties uqProperties = new(dto.Name);
        await ValidateProperties(new ProductionSiteCreateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, ProductionSiteExpressions.GetUqPredicates(uqProperties));
    }
}



