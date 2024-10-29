using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Zpl.Templates;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.References.Template.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Validators;

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