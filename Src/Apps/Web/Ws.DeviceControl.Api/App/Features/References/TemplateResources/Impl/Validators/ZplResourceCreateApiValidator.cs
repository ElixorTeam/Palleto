using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Zpl.ZplResources;
using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Validators;

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



