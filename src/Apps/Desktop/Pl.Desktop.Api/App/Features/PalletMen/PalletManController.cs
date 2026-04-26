using Pl.Desktop.Api.App.Features.PalletMen.Common;
using Pl.Desktop.Models.Features.PalletMen;

namespace Pl.Desktop.Api.App.Features.PalletMen;

[ApiController]
[Authorize(PolicyEnum.Pc)]
[Route(ApiEndpoints.PalletMen)]
public sealed class PalletManController(IPalletManService palletManService)
{
    #region Queries

    [HttpGet]
    public Task<PalletManDto> GetPalletManByCode([FromQuery(Name = "code")] string code) =>
        palletManService.GetByCodeAsync(code);

    #endregion
}