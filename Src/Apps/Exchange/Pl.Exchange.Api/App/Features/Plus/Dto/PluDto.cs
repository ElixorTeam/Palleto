namespace Pl.Exchange.Api.App.Features.Plus.Dto;

[Serializable]
public sealed record PluDto : BaseDto
{
    [XmlAttribute("Name")]
    public string Name = string.Empty;

    [XmlAttribute("FullName")]
    public string FullName = string.Empty;

    [XmlAttribute("Description")]
    public string Description = string.Empty;

    [XmlAttribute("Number")]
    public short Number;

    [XmlAttribute("BundleCount")]
    public short BundleCount;

    [XmlAttribute("ShelfLife")]
    public short ShelfLifeDays;

    [XmlAttribute("IsDelete")]
    public bool IsDelete;

    #region Fk

    [XmlAttribute("BrandUid")]
    public Guid BrandUid;

    [XmlAttribute("BoxUid")]
    public Guid BoxUid;

    [XmlAttribute("ClipUid")]
    public Guid ClipUid;

    [XmlAttribute("BundleUid")]
    public Guid BundleUid;

    #endregion

    [XmlAttribute("Weight")]
    public decimal Weight;

    [XmlAttribute("Ean13")]
    public string Ean13 = string.Empty;

    [XmlAttribute("Itf14")]
    public string Itf14 = string.Empty;

    [XmlAttribute("IsWeight")]
    public bool IsWeight;

    [XmlAttribute("StorageMethod")]
    public string StorageMethod = string.Empty;
}