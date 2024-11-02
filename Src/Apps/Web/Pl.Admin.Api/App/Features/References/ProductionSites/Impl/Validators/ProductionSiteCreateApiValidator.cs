using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.ProductionSites.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Ref.ProductionSites;
using Pl.Admin.Models.Features.References.ProductionSites.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.References.ProductionSites.Impl.Validators;

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



