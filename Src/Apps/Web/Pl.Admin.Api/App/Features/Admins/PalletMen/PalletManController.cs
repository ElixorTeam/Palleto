using Pl.Admin.Api.App.Features.Admins.PalletMen.Common;
using Pl.Admin.Models.Features.Admins.PalletMen.Commands;
using Pl.Admin.Models.Features.Admins.PalletMen.Queries;

namespace Pl.Admin.Api.App.Features.Admins.PalletMen;

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
    public Task<PalletManDto> Create([FromBody] PalletManCreateDto dto) =>
        palletManService.CreateAsync(dto);

    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) => palletManService.DeleteAsync(id);

    #endregion
}