using Pl.Mobile.Models.Features.Pallets;

namespace Pl.Mobile.Api.App.Features.Pallets.Common;

public interface IPalletService
{
    #region Commands

    void Move(PalletsMoveDto palletMoveDto);

    #endregion
}