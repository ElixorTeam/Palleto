// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PrintersResourcesFks;

/// <summary>
/// Table validation "ZebraPrinterResourceRef".
/// </summary>
public sealed class WsSqlPrinterResourceFkValidator : WsSqlTableValidator<WsSqlPrinterResourceFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPrinterResourceFkValidator() : base(false, false)
    {
        RuleFor(item => item.Printer)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPrinterValidator());
        RuleFor(item => item.TemplateResource)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlTemplateResourceValidator());
    }
}