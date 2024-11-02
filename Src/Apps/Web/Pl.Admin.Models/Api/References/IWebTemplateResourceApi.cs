using Pl.Admin.Models.Features.References.TemplateResources.Commands;
using Pl.Admin.Models.Features.References.TemplateResources.Queries;

namespace Pl.Admin.Models.Api.References;

public interface IWebTemplateResourceApi
{
    #region Queries

    [Get("/template-resources")]
    Task<TemplateResourceDto[]> GetResources();

    [Get("/template-resources/{id}")]
    Task<TemplateResourceDto> GetResourceByUid(Guid id);

    [Get("/template-resources/{id}/body")]
    Task<TemplateResourceBodyDto> GetTemplateResourceBody(Guid id);

    #endregion

    #region Commands

    [Delete("/template-resources/{id}")]
    Task DeleteResource(Guid id);

    [Post("/template-resources")]
    Task<TemplateResourceDto> CreateResource([Body] ZplResourceCreateDto createDto);

    [Put("/template-resources/{id}")]
    Task<TemplateResourceDto> UpdateResource(Guid id, [Body] ZplResourceUpdateDto updateDto);

    #endregion
}