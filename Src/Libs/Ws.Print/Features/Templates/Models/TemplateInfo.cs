using Ws.Print.Features.Barcodes.Models;

namespace Ws.Print.Features.Templates.Models;

public record TemplateInfo(
    string Template,
    ushort Width,
    ushort Height,
    ushort Rotate,
    List<BarcodeVar> BarcodeTopTemplate,
    List<BarcodeVar> BarcodeRightTemplate,
    List<BarcodeVar> BarcodeBottomTemplate
);