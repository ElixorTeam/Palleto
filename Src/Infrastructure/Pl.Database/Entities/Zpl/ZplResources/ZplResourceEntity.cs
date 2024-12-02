using Pl.Shared.Enums;

namespace Pl.Database.Entities.Zpl.ZplResources;

public sealed class ZplResourceEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Zpl { get; set; } = string.Empty;
    public ZplResourceType Type { get; set; } = ZplResourceType.Text;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}