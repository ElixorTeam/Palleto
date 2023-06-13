// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewRefModels;

[DebuggerDisplay("{ToString()}")]
public sealed class WsSqlViewPluNestingModel : WsSqlViewBase
{
    #region Public and private fields, properties, constructor

    public bool IsMarked { get; init; }
    public bool IsDefault { get; init; }
    public short BundleCount { get; init; }
    public decimal WeightMax { get; init; }
    public decimal WeightMin { get; init; }
    public decimal WeightNom { get; init; }
    public Guid PluUid { get; init; }
    public Guid PluUid1C { get; init; }
    public bool PluIsMarked { get; init; }
    public bool PluIsWeight { get; init; }
    public bool PluIsGroup { get; init; }
    public ushort PluNumber { get; init; }
    public string PluName { get; init; }
    public short PluShelfLifeDays { get; init; }
    public string PluGtin { get; init; }
    public string PluEan13 { get; init; }
    public string PluItf14 { get; init; }
    public Guid BundleUid { get; init; }
    public Guid BundleUid1C { get; init; }
    public bool BundleIsMarked { get; init; }
    public string BundleName { get; init; }
    public decimal BundleWeight { get; init; }
    public Guid BoxUid { get; init; }
    public Guid BoxUid1C { get; init; }
    public bool BoxIsMarked { get; init; }
    public string BoxName { get; init; }
    public decimal BoxWeight { get; init; }
    public decimal TareWeight { get; init; }
    public string TareWeightWithKg => $"{TareWeight} {WsLocaleCore.LabelPrint.WeightUnitKg}";
    public string TareWeightDescription => $"{BoxName} + ({BundleName} * {BundleCount})";
    public string TareWeightValue => $"{BoxWeight} + ({BundleWeight} * {BundleCount})";
    public string PluNumberName => $"{PluNumber} | {PluName}";

    public WsSqlViewPluNestingModel() : this(Guid.Empty, default, default, default,
        default, default, default, Guid.Empty, Guid.Empty, default, default, default, default, string.Empty,
        default, string.Empty, string.Empty, string.Empty,
        Guid.Empty, Guid.Empty, default, string.Empty, default,
        Guid.Empty, Guid.Empty, default, string.Empty, default, default)
    { }

    public WsSqlViewPluNestingModel(Guid uid, bool isMarked, bool isDefault, short bundleCount,
        decimal weightMax, decimal weightMin, decimal weightNom,
        Guid pluUid, Guid pluUid1C, bool pluIsMarked, bool pluIsWeight, bool pluIsGroup, ushort pluNumber, string pluName,
        short pluShelfLifeDays, string pluGtin, string pluEan13, string pluItf14,
        Guid bundleUid, Guid bundleUid1C, bool bundleIsMarked, string bundleName, decimal bundleWeight,
        Guid boxUid, Guid boxUid1C, bool boxIsMarked, string boxName, decimal boxWeight, decimal tareWeight) : base(uid)
    {
        IsMarked = isMarked;
        IsDefault = isDefault;
        BundleCount = bundleCount;
        WeightMax = weightMax;
        WeightMin = weightMin;
        WeightNom = weightNom;
        PluUid = pluUid;
        PluUid1C = pluUid1C;
        PluIsMarked = pluIsMarked;
        PluIsWeight = pluIsWeight;
        PluIsGroup = pluIsGroup;
        PluNumber = pluNumber;
        PluName = pluName;
        PluShelfLifeDays = pluShelfLifeDays;
        PluGtin = pluGtin;
        PluEan13 = pluEan13;
        PluItf14 = pluItf14;
        BundleUid = bundleUid;
        BundleUid1C = bundleUid1C;
        BundleIsMarked = bundleIsMarked;
        BundleName = bundleName;
        BundleWeight = bundleWeight;
        BoxUid = boxUid;
        BoxUid1C = boxUid1C;
        BoxIsMarked = boxIsMarked;
        BoxName = boxName;
        BoxWeight = boxWeight;
        TareWeight = tareWeight;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{TareWeightDescription} | {TareWeight}";
    
    public string GetSmartName() => TareWeight > 0 ? $"{TareWeight} {WsLocaleCore.LabelPrint.WeightUnitKg} | {PluName}" : "- 0 -";
    
    #endregion
}