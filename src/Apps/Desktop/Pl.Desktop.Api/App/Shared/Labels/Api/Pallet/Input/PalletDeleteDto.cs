using System.Xml.Serialization;

namespace Pl.Desktop.Api.App.Shared.Labels.Api.Pallet.Input;

[XmlRoot(ElementName = "PalletType")]
public class PalletDeleteWrapper
{
    [XmlElement(ElementName = "Record")]
    public PalletDeleteDto Pallet { get; set; }
}

public class PalletDeleteDto
{
    [XmlAttribute(AttributeName = "Number")]
    public string Number { get; set; } = string.Empty;

    [XmlAttribute(AttributeName = "IsDelete")]
    public bool IsDelete { get; set; }
}