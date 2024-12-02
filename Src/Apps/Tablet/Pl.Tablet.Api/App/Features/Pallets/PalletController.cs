using Pl.Tablet.Api.App.Features.Pallets.Common;
using Pl.Tablet.Models.Features.Pallets.Input;
using Pl.Tablet.Models.Features.Pallets.Output;

namespace Pl.Tablet.Api.App.Features.Pallets;

[ApiController]
[Route(ApiEndpoints.Pallets)]
public sealed class PalletController(IPalletService palletService)
{
    #region Commands

    [HttpPost]
    public PalletDto GetPluByCode([FromBody] PalletCreateDto palletCreateDto) => palletService.Create(palletCreateDto);

    #endregion
}