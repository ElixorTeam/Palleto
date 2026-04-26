using System.ComponentModel.DataAnnotations;

namespace Pl.Desktop.Models.Features.Labels.Input;

public sealed record CreateWeightLabelDto
{
    [Required]
    [Range(1, 999)]
    [JsonPropertyName("kneading")]
    public required ushort Kneading { get; init; }

    [Required]
    [Range(0.1, 99.000)]
    [JsonPropertyName("weightNet")]
    public required decimal WeightNet { get; init; }

    [Required]
    [JsonPropertyName("weightTare")]
    [Range(0.1, 99.000)]
    public required decimal WeightTare { get; init; }

    [Required]
    [JsonPropertyName("productDt")]
    public required DateTime ProductDt { get; init; }
}