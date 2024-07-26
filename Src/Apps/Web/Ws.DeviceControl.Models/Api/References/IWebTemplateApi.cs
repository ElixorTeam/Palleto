using Ws.DeviceControl.Models.Dto.References.Template.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.Template.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.Template.Queries;

namespace Ws.DeviceControl.Models.Api.References;

public interface IWebTemplateApi
{
    #region Queries

    [Get("/templates/proxy?plu={pluId}")]
    Task<ProxyDto[]> GetProxyTemplatesByPlu(Guid pluId);

    [Get("/templates")]
    Task<TemplateDto[]> GetTemplates();

    [Get("/templates/{id}")]
    Task<TemplateDto> GetTemplateByUid(Guid id);

    #endregion

    #region Commands

    [Delete("/templates/{id}")]
    Task<bool> DeleteTemplate(Guid id);

    [Post("/templates")]
    Task<TemplateDto> CreateTemplate([Body] TemplateCreateDto createDto);

    [Post("/templates/{id}")]
    Task<TemplateDto> UpdateTemplate(Guid id, [Body] TemplateUpdateDto updateDto);

    #endregion
}