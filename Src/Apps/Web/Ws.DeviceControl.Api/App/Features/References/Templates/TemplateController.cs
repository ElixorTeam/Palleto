using Ws.DeviceControl.Api.App.Features.References.Templates.Common;
using Ws.DeviceControl.Models.Features.References.Template.Commands;
using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace Ws.DeviceControl.Api.App.Features.References.Templates;

[ApiController]
[Route(ApiEndpoints.Templates)]
public sealed class TemplateController(ITemplateService templateService)
{
    #region Queries

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet]
    public Task<TemplateDto[]> GetAll() =>
        templateService.GetAllAsync();

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}")]
    public Task<TemplateDto> GetById([FromRoute] Guid id) =>
        templateService.GetByIdAsync(id);

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}/body")]
    public Task<TemplateBodyDto> GetBodyById([FromRoute] Guid id) =>
        templateService.GetBodyByIdAsync(id);

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxiesByIsWeight([FromQuery(Name = "isWeight")] bool isWeight) =>
        templateService.GetProxiesByIsWeightAsync(isWeight);

    [HttpGet("{id:guid}/barcodes")]
    public Task<BarcodeItemWrapper> GetBarcodes([FromRoute] Guid id) =>
        templateService.GetBarcodeTemplates(id);

    #endregion

    #region Commands

    [Authorize(PolicyEnum.Admin)]
    [HttpPost]
    public Task<TemplateDto> Create([FromBody] TemplateCreateDto dto) =>
        templateService.CreateAsync(dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPut("{id:guid}")]
    public Task<TemplateDto> Update([FromRoute] Guid id, [FromBody] TemplateUpdateDto dto) =>
        templateService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) =>
        templateService.DeleteAsync(id);

    [Authorize(PolicyEnum.Admin)]
    [HttpPut("{id:guid}/barcodes")]
    public Task<BarcodeItemWrapper> UpdateBarcodes([FromRoute] Guid id, [FromBody] BarcodeItemWrapper dto) =>
        templateService.UpdateBarcodeTemplates(id, dto);

    #endregion
}