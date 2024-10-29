using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Zpl.Templates;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.References.Template.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Validators;

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



