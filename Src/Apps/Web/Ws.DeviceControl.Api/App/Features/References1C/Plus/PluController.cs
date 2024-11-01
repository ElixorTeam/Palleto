using Ws.DeviceControl.Api.App.Features.References1C.Plus.Common;
using Ws.DeviceControl.Models.Features.References1C.Plus.Commands.Update;
using Ws.DeviceControl.Models.Features.References1C.Plus.Queries;

namespace Ws.DeviceControl.Api.App.Features.References1C.Plus;

[ApiController]
[Route(ApiEndpoints.Plu)]
public sealed class PluController(IPluService pluService)
{
    #region Queries

    [HttpGet]
    public Task<PluDto[]> GetAll() =>
        pluService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<PluDto> GetById([FromRoute] Guid id) =>
        pluService.GetByIdAsync(id);

    [HttpGet("{id:guid}/characteristics")]
    public Task<CharacteristicDto[]> GetCharacteristics([FromRoute] Guid id) =>
        pluService.GetCharacteristics(id);

    #endregion

    #region Commands

    [HttpPut("{id:guid}")]
    public Task<PluDto> Update([FromRoute] Guid id, [FromBody] PluUpdateDto dto) =>
        pluService.Update(id, dto);

    #endregion
}