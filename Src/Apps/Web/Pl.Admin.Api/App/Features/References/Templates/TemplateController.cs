using Pl.Admin.Api.App.Features.References.Templates.Common;
using Pl.Admin.Models.Features.References.Template.Commands;
using Pl.Admin.Models.Features.References.Template.Queries;
using Pl.Admin.Models.Features.References.Template.Universal;

namespace Pl.Admin.Api.App.Features.References.Templates;

[ApiController]
[Route(ApiEndpoints.Templates)]
public sealed class TemplateController(ITemplateService templateService)
{
    #region Queries

    [Authorize(PolicyEnum.Support)]
    [HttpGet]
    public Task<TemplateDto[]> GetAll() =>
        templateService.GetAllAsync();

    [Authorize(PolicyEnum.Support)]
    [HttpGet("{id:guid}")]
    public Task<TemplateDto> GetById([FromRoute] Guid id) =>
        templateService.GetByIdAsync(id);

    [Authorize(PolicyEnum.Support)]
    [HttpGet("{id:guid}/body")]
    public Task<TemplateBodyDto> GetBodyById([FromRoute] Guid id) =>
        templateService.GetBodyByIdAsync(id);

    [HttpGet("proxy")]
    public Task<ProxyDto[]> GetProxiesByIsWeight([FromQuery(Name = "isWeight")] bool isWeight) =>
        templateService.GetProxiesByIsWeightAsync(isWeight);

    [HttpGet("{id:guid}/barcodes")]
    public Task<BarcodeItemWrapper> GetBarcodes([FromRoute] Guid id) =>
        templateService.GetBarcodeTemplates(id);

    #endregion

    #region Commands

    [Authorize(PolicyEnum.Developer)]
    [HttpPost]
    public Task<TemplateDto> Create([FromBody] TemplateCreateDto dto) =>
        templateService.CreateAsync(dto);

    [Authorize(PolicyEnum.Developer)]
    [HttpPut("{id:guid}")]
    public Task<TemplateDto> Update([FromRoute] Guid id, [FromBody] TemplateUpdateDto dto) =>
        templateService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Developer)]
    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) =>
        templateService.DeleteAsync(id);

    [Authorize(PolicyEnum.Developer)]
    [HttpPut("{id:guid}/barcodes")]
    public Task<BarcodeItemWrapper> UpdateBarcodes([FromRoute] Guid id, [FromBody] BarcodeItemWrapper dto) =>
        templateService.UpdateBarcodeTemplates(id, dto);

    #endregion
}