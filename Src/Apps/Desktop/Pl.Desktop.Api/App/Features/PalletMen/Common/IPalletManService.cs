using Pl.Desktop.Models.Features.PalletMen;

namespace Pl.Desktop.Api.App.Features.PalletMen.Common;

public interface IPalletManService
{
    #region Queries

    Task<PalletManDto> GetByCodeAsync(string code);

    #endregion
}