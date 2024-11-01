using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPalletManApi
{
    #region Queries

    [Get("/pallet-men")]
    Task<PalletManDto> GetPalletManByCode(string code);

    #endregion
}