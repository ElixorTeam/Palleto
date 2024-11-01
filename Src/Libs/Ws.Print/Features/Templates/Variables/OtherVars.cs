// ReSharper disable NotAccessedPositionalProperty.Global

using Ws.Print.Shared.ValueTypes;

namespace Ws.Print.Features.Templates.Variables;

/// <summary>
/// Represents variables for pallets in templates (Be careful).
/// </summary>
public record PalletVars(ushort Order, string Number);

/// <summary>
/// Represents variables for arms in templates (Be careful).
/// </summary>
public record ArmVars(int Number, string Name, string Address);

/// <summary>
/// Represents variables for PLU in templates (Be careful).
/// </summary>
public record PluVars(string Name, ushort Number, string Description);

/// <summary>
/// Represents variables for Barcodes in templates (Be careful).
/// </summary>
public record BarcodesVars(BarcodeResult Top, BarcodeResult Bottom, BarcodeResult Right);