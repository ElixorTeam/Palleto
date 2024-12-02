namespace Pl.Exchange.Api.App.Features.Boxes.Dto;

[XmlRoot("Boxes")]
public sealed record BoxesWrapper
{
    [XmlElement("Box")]
    public HashSet<BoxDto> Boxes { get; set; } = [];
}