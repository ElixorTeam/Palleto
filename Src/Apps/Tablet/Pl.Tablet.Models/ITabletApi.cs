using Pl.Tablet.Models.Api;

namespace Pl.Tablet.Models;

public interface ITabletApi :
    ITabletArmApi,
    ITabletPluApi,
    ITabletUserApi,
    ITabletPalletApi;