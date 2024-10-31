using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Common;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources;

[ApiController]
[Route(ApiEndpoints.TemplateResources)]
public sealed class ZplResourceController(IZplResourceService zplResourceService)
{
    #region Queries

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet]
    public Task<TemplateResourceDto[]> GetAll() =>
        zplResourceService.GetAllAsync();

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}")]
    public Task<TemplateResourceDto> GetById([FromRoute] Guid id) =>
        zplResourceService.GetByIdAsync(id);

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}/body")]
    public Task<TemplateResourceBodyDto> GetBodyById([FromRoute] Guid id) =>
        zplResourceService.GetBodyByIdAsync(id);

    #endregion

    #region Commands

    [Authorize(PolicyEnum.Admin)]
    [HttpPost]
    public Task<TemplateResourceDto> Create([FromBody] ZplResourceCreateDto dto) =>
        zplResourceService.CreateAsync(dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPut("{id:guid}")]
    public Task<TemplateResourceDto> Update([FromRoute] Guid id, [FromBody] ZplResourceUpdateDto dto) =>
        zplResourceService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) =>
        zplResourceService.DeleteAsync(id);

    #endregion
}