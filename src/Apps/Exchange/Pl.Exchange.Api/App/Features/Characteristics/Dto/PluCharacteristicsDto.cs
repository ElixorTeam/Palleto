namespace Pl.Exchange.Api.App.Features.Characteristics.Dto;

public sealed record PluCharacteristicsDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlElement("Characteristic")]
    public HashSet<CharacteristicDto> Characteristics { get; set; } = [];
}