using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Zpl.ZplResources;
using Pl.Admin.Models.Features.References.TemplateResources.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Validators;

public class ZplResourceCreateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiCreateValidator<ZplResourceEntity, ZplResourceCreateDto>
{
    public override async Task ValidateAsync(DbSet<ZplResourceEntity> dbSet, ZplResourceCreateDto dto)
    {
        UqZplResourceProperties uqProperties = new(dto.Name);
        await ValidateProperties(new ZplResourceCreateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, ZplResourceExpressions.GetUqPredicates(uqProperties));
    }
}



