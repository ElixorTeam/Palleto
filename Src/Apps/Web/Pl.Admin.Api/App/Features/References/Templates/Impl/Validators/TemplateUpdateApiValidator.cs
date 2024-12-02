using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.References.Templates.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.Templates.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Zpl.Templates;
using Pl.Admin.Models.Features.References.Template.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.References.Templates.Impl.Validators;

public class TemplateUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiUpdateValidator<TemplateEntity, TemplateUpdateDto, Guid>
{
    public override async Task<TemplateEntity> ValidateAndGetAsync(DbSet<TemplateEntity> dbSet, TemplateUpdateDto dto, Guid id)
    {
        UqTemplateProperties uqProperties = new(dto.Name);
        await ValidateProperties(new TemplateUpdateValidator(wsDataLocalizer), dto);
        return await ValidatePredicatesAsync(dbSet, TemplateExpressions.GetUqPredicates(uqProperties), i => i.Id == id);
    }
}