using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Zpl.ZplResources;
using Pl.Admin.Models.Features.References.TemplateResources.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Validators;

public class ZplResourceUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiUpdateValidator<ZplResourceEntity, ZplResourceUpdateDto, Guid>
{
    public override async Task<ZplResourceEntity> ValidateAndGetAsync(DbSet<ZplResourceEntity> dbSet, ZplResourceUpdateDto dto, Guid id)
    {
        UqZplResourceProperties uqProperties = new(dto.Name);
        await ValidateProperties(new ZplResourceUpdateValidator(wsDataLocalizer), dto);
        return await ValidatePredicatesAsync(dbSet, ZplResourceExpressions.GetUqPredicates(uqProperties), i => i.Id == id);
    }
}