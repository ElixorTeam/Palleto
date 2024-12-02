namespace Pl.Exchange.Api.App.Features.Characteristics.Dto;

[Serializable]
public sealed record CharacteristicDto : BaseDto
{
    [XmlAttribute("BoxUid")]
    public Guid BoxUid;

    [XmlAttribute("IsDelete")]
    public bool IsDelete;

    [XmlAttribute("BundleCount")]
    public short BundleCount;

    [XmlAttribute("Name")]
    public string Name = string.Empty;
}