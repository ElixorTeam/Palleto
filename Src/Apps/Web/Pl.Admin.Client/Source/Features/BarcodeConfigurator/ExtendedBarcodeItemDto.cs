using Pl.Admin.Models.Features.References.Template.Universal;

namespace Pl.Admin.Client.Source.Features.BarcodeConfigurator;

public sealed record ExtendedBarcodeItemDto: BarcodeItemDto
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Example { get; set; } = string.Empty;
    public bool IsConst { get; set; } = true;
    public int DefaultLength { get; set; } = -1;
    public string CachedMask { get; set; } = string.Empty;
}