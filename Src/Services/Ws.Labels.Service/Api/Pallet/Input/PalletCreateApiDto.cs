using System.Xml.Serialization;

namespace Ws.Labels.Service.Api.Pallet.Input;

[Serializable]
public record PalletCreateApiDto
{
    [XmlAttribute("Organization")]
    public required string Organization;

    [XmlAttribute("PluUid")]
    public required Guid PluUid;

    [XmlAttribute("PalletManUid")]
    public required Guid PalletManUid;

    [XmlAttribute("WarehouseUid")]
    public required Guid WarehouseUid;

    [XmlAttribute("CharacteristicUid")]
    public required Guid CharacteristicUid;

    [XmlAttribute("Barcode")]
    public required string Barcode;

    [XmlAttribute("ArmNumber")]
    public required uint ArmNumber;

    [XmlAttribute("Kneading")]
    public required ushort Kneading;

    [XmlAttribute("TrayWeightKg")]
    public required decimal TrayWeightKg;

    [XmlAttribute("NetWeightKg")]
    public decimal NetWeightKg;

    [XmlAttribute("GrossWeightKg")]
    public decimal GrossWeightKg;

    [XmlAttribute("CreatedAt")]
    public required DateTime CreatedAt;

    [XmlArray("Labels"), XmlArrayItem("Label")]
    public required List<LabelCreateApiDto> Labels;
}