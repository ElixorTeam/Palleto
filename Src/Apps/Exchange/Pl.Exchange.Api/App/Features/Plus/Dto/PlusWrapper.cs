namespace Pl.Exchange.Api.App.Features.Plus.Dto;

[XmlRoot("Plus")]
public sealed class PlusWrapper
{
    [XmlElement("Plu")]
    public HashSet<PluDto> Plus { get; set; } = [];
}