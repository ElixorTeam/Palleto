using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.ProductionSites.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Ref.ProductionSites;
using Pl.Admin.Models.Features.References.ProductionSites.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.References.ProductionSites.Impl.Validators;

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