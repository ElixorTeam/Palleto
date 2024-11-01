using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen;

[ApiController]
[Authorize(PolicyEnum.Pc)]
[Route(ApiEndpoints.PalletMen)]
public sealed class PalletManController(IPalletManService palletManService)
{
    #region Queries

    [HttpGet]
    public Task<PalletMan> GetPalletManByCode([FromQuery(Name = "code")] string code) =>
        palletManService.GetByCodeAsync(code);

    #endregion
}