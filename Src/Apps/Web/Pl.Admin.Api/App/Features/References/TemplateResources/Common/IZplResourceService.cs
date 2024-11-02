using Pl.Admin.Models.Features.References.TemplateResources.Commands;
using Pl.Admin.Models.Features.References.TemplateResources.Queries;

namespace Pl.Admin.Api.App.Features.References.TemplateResources.Common;

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