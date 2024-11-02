using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.References.Templates.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.Templates.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Zpl.Templates;
using Pl.Admin.Models.Features.References.Template.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.References.Templates.Impl.Validators;

public class TemplateCreateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiCreateValidator<TemplateEntity, TemplateCreateDto>
{
    public override async Task ValidateAsync(DbSet<TemplateEntity> dbSet, TemplateCreateDto dto)
    {
        UqTemplateProperties uqProperties = new(dto.Name);
        await ValidateProperties(new TemplateCreateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, TemplateExpressions.GetUqPredicates(uqProperties));
    }
}



