using Ws.DeviceControl.Models.Features.References.Template.Commands;
using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Common;

public interface ITemplateService :
    IGetById<TemplateDto>,
    IGetAll<TemplateDto>,
    IDeleteById
{
    #region Queries

    Task<List<ProxyDto>> GetProxiesByIsWeightAsync(bool isWeight);
    Task<TemplateBodyDto> GetBodyByIdAsync(Guid id);
    Task<BarcodeItemWrapper> GetBarcodeTemplates(Guid id);

    #endregion

    #region Commands

    Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto dto);

    Task<TemplateDto> CreateAsync(TemplateCreateDto dto);

    Task<BarcodeItemWrapper> UpdateBarcodeTemplates(Guid id, BarcodeItemWrapper barcodes);

    #endregion
}