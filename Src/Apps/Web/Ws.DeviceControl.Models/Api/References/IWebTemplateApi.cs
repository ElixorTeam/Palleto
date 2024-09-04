using Ws.DeviceControl.Models.Features.References.Template.Commands.Create;
using Ws.DeviceControl.Models.Features.References.Template.Commands.Update;
using Ws.DeviceControl.Models.Features.References.Template.Queries;

namespace Ws.DeviceControl.Models.Api.References;

public interface IWebTemplateApi
{
    #region Queries

    [Get("/templates")]
    Task<TemplateDto[]> GetTemplates();

    [Get("/templates/{id}/body")]
    Task<TemplateBodyDto> GetTemplateBody(Guid id);

    [Get("/templates/{id}")]
    Task<TemplateDto> GetTemplateByUid(Guid id);

    [Get("/templates/proxy?isWeight={isWeight}")]
    Task<ProxyDto[]> GetProxyTemplatesByPluType(bool isWeight);

    #endregion

    #region Commands

    [Post("/templates/{id}/delete")]
    Task DeleteTemplate(Guid id);

    [Post("/templates")]
    Task<TemplateDto> CreateTemplate([Body] TemplateCreateDto createDto);

    [Post("/templates/{id}")]
    Task<TemplateDto> UpdateTemplate(Guid id, [Body] TemplateUpdateDto updateDto);

    #endregion
}