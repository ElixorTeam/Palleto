namespace Pl.Print.Features.Barcodes.Models;

public sealed record BarcodeVarInfo(Type Type, string Property, string Format, object Example, bool IsRepeatable);