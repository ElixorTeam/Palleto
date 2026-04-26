namespace Pl.Exchange.Api.App.Features.Brands.Dto;

[XmlRoot("Brands")]
public sealed record BrandsWrapper
{
    [XmlElement("Brand")]
    public HashSet<BrandDto> Brands { get; set; } = [];
}