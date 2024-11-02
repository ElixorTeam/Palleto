using Pl.Desktop.Models.Api;

namespace Pl.Desktop.Models;

public interface IDesktopApi :
    IDesktopArmApi,
    IDesktopPalletApi,
    IDesktopPalletManApi,
    IDesktopPluPieceApi,
    IDesktopPluWeightApi;