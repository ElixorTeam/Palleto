using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Common;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen;

[ApiController]
[Route(ApiEndpoints.PalletMen)]
[Authorize(PolicyEnum.Support)]
public sealed class PalletManController(IPalletManService palletManService)
{
    #region Queries

    [HttpGet]
    public Task<PalletManDto[]> GetAllByProdSite([FromQuery(Name = "productionSite")] Guid prodSiteId) =>
        palletManService.GetAllByProdSiteAsync(prodSiteId);

    [HttpGet("{id:guid}")]
    public Task<PalletManDto> GetById([FromRoute] Guid id) =>
        palletManService.GetByIdAsync(id);

    #endregion

    #region Commands

    [HttpPut("{id:guid}")]
    public Task<PalletManDto> Update([FromRoute] Guid id, [FromBody] PalletManUpdateDto dto) =>
        palletManService.UpdateAsync(id, dto);

    [HttpPost]
    [Authorize(PolicyEnum.SeniorSupport)]
    public Task<PalletManDto> Create([FromBody] PalletManCreateDto dto) =>
        palletManService.CreateAsync(dto);

    [HttpDelete("{id:guid}")]
    [Authorize(PolicyEnum.SeniorSupport)]
    public Task Delete([FromRoute] Guid id) => palletManService.DeleteAsync(id);

    #endregion
}