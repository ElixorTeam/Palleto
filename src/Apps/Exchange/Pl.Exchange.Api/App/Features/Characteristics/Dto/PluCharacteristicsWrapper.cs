namespace Pl.Exchange.Api.App.Features.Characteristics.Dto;

[XmlRoot("Characteristics")]
public sealed record PluCharacteristicsWrapper
{
    [XmlElement("Plu")]
    public HashSet<PluCharacteristicsDto> PluCharacteristics { get; set; } = [];
}