using Pl.Desktop.Models.Features.PalletMen;

namespace Pl.Desktop.Models.Api;

public interface IDesktopPalletManApi
{
    #region Queries

    [Get("/pallet-men")]
    Task<PalletManDto> GetPalletManByCode(string code);

    #endregion
}