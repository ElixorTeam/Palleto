using Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources.Common;

public interface IZplResourceService :
    IGetById<TemplateResourceDto>,
    IGetAll<TemplateResourceDto>,
    IDeleteById
{
    #region Queries

    public Task<TemplateResourceBodyDto> GetBodyByIdAsync(Guid id);

    #endregion

    #region Commmands

    Task<TemplateResourceDto> CreateAsync(ZplResourceCreateDto dto);
    Task<TemplateResourceDto> UpdateAsync(Guid id, ZplResourceUpdateDto dto);

    #endregion
}