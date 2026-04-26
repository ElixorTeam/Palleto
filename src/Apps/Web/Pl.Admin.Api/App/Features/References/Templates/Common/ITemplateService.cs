using Pl.Admin.Models.Features.References.Template.Commands;
using Pl.Admin.Models.Features.References.Template.Queries;
using Pl.Admin.Models.Features.References.Template.Universal;

namespace Pl.Admin.Api.App.Features.References.Templates.Common;

public interface ITemplateService :
    IGetById<TemplateDto>,
    IGetAll<TemplateDto>,
    IDeleteById
{
    #region Queries

    Task<ProxyDto[]> GetProxiesByIsWeightAsync(bool isWeight);
    Task<TemplateBodyDto> GetBodyByIdAsync(Guid id);
    Task<BarcodeItemWrapper> GetBarcodeTemplates(Guid id);

    #endregion

    #region Commands

    Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto dto);

    Task<TemplateDto> CreateAsync(TemplateCreateDto dto);

    Task<BarcodeItemWrapper> UpdateBarcodeTemplates(Guid id, BarcodeItemWrapper barcodes);

    #endregion
}