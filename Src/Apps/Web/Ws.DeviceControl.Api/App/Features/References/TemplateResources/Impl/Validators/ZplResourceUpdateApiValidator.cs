using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Zpl.ZplResources;
using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Validators;

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