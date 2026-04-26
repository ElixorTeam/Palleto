using Pl.Shared.ValueTypes;

namespace Pl.Database.Entities.Zpl.Templates;

public sealed class TemplateEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsWeight { get; set; }

    public short Width { get; set; }
    public short Height { get; set; }
    public short Rotate { get; set; }

    public List<BarcodeItem> BarcodeTopBody { get; set; } = [];
    public List<BarcodeItem> BarcodeBottomBody { get; set; } = [];
    public List<BarcodeItem> BarcodeRightBody { get; set; } = [];

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}