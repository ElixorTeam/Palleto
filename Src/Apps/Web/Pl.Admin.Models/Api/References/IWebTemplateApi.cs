using Pl.Admin.Models.Features.References.Template.Commands;
using Pl.Admin.Models.Features.References.Template.Queries;
using Pl.Admin.Models.Features.References.Template.Universal;

namespace Pl.Admin.Models.Api.References;

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

    #region Barcodes

    [Get("/templates/{id}/barcodes/vars")]
    Task<BarcodeVarDto[]> GetBarcodeVariables(Guid id);

    [Get("/templates/{id}/barcodes")]
    Task<BarcodeItemWrapper> GetBarcodes(Guid id);

    #endregion

    #endregion

    #region Commands

    [Delete("/templates/{id}")]
    Task DeleteTemplate(Guid id);

    [Post("/templates")]
    Task<TemplateDto> CreateTemplate([Body] TemplateCreateDto createDto);

    [Put("/templates/{id}")]
    Task<TemplateDto> UpdateTemplate(Guid id, [Body] TemplateUpdateDto updateDto);

    [Put("/templates/{id}/barcodes")]
    Task<BarcodeItemWrapper> UpdateBarcodeVariables(Guid id, [Body] BarcodeItemWrapper body);

    #endregion
}