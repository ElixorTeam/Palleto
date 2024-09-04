using System.Xml.Serialization;

namespace Ws.PalychExchange.Api.Features.Boxes.Dto;

[XmlRoot("Boxes")]
public sealed record BoxesWrapper
{
    [XmlElement("Box")]
    public List<BoxDto> Boxes { get; set; } = [];
}