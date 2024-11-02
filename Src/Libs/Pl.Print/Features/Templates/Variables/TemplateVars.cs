// ReSharper disable NotAccessedField.Global, UnusedMember.Global

using Pl.Shared.Constants;

namespace Pl.Print.Features.Templates.Variables;

/// <summary>
/// DON'T TOUCH (THIS IS VARIABLES FOR TEMPLATES) BE CAREFUL
/// </summary>
public class TemplateVars(
    PluVars plu,
    ArmVars arm,
    PalletVars pallet,
    BarcodesVars barcodes,
    ushort bundleCount,
    ushort kneading,
    decimal weightNet,
    decimal weightGross,
    DateTime productDt,
    DateTime expirationDt
    )
{
    #region Vars

    public readonly PluVars Plu = plu;
    public readonly ArmVars Arm = arm;
    public readonly BarcodesVars Barcodes = barcodes;
    public PalletVars Pallet = pallet;

    #endregion

    #region Other

    public readonly string WeightNet = weightNet.ToString("0.000", Cultures.Ru);
    public readonly string WeightGross = weightGross.ToString("0.000", Cultures.Ru);

    public readonly ushort BundleCount = bundleCount;
    public readonly string Kneading = $"{kneading:D3}";

    public readonly string ProductDate = $"{productDt:dd.MM.yyyy}";
    public readonly string ExpirationDate = $"{expirationDt:dd.MM.yyyy}";

    #endregion
};