using TscZebra.Plugin.Abstractions.Enums;

namespace Pl.Database.Entities.Ref.Printers;

public sealed class PrinterEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public PrinterTypes Type { get; set; } = PrinterTypes.Tsc;
    public IPAddress Ip { get; set; } = IPAddress.None;

    #region Fk

    public Guid ProductionSiteId { get; set; }
    public ProductionSiteEntity ProductionSite { get; set; } = new();

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}