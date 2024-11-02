using System.Xml.Serialization;

namespace Pl.Desktop.Api.App.Shared.Labels.Api.Pallet.Input;

[Serializable]
public record LabelCreateApiDto
{
    [XmlAttribute("BarcodeTop")]
    public required string BarcodeTop { get; set; } = string.Empty;

    [XmlAttribute("BarcodeRight")]
    public required string BarcodeRight { get; set; } = string.Empty;

    [XmlAttribute("BarcodeBottom")]
    public required string BarcodeBottom { get; set; } = string.Empty;

    [XmlAttribute("Kneading")]
    public required ushort Kneading;

    [XmlAttribute("NetWeightKg")]
    public required decimal NetWeightKg { get; set; }

    [XmlAttribute("GrossWeightKg")]
    public required decimal GrossWeightKg { get; set; }
}