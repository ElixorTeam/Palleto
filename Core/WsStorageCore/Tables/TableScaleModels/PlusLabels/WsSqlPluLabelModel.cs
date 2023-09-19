// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.Tables.TableScaleModels.PlusLabels;

/// <summary>
/// Table "PLUS_LABELS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluLabelModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    public virtual WsSqlPluWeighingModel? PluWeighing { get; set; }
    public virtual WsSqlPluScaleModel PluScale { get; set; }
    public virtual string Zpl { get; set; }
    public virtual XmlDocument? Xml { get; set; }
    public virtual DateTime ProductDt { get; set; }
    public virtual DateTime ExpirationDt
    {
        get => PluScale.IsNew ? DateTime.MinValue : ProductDt.AddDays(PluScale.Plu.ShelfLifeDays);
        set => _ = value;
    }

    public WsSqlPluLabelModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        PluWeighing = null;
        PluScale = new();
        Zpl = string.Empty;
        Xml = null;
        ProductDt = DateTime.MinValue;
        ExpirationDt = DateTime.MinValue;
    }

    public WsSqlPluLabelModel(WsSqlPluLabelModel item) : base(item)
    {
        IsMarked = item.IsMarked;
        PluWeighing = item.PluWeighing is null ? null : new(item.PluWeighing);
        PluScale = new(item.PluScale);
        Zpl = item.Zpl;
        Xml = item.Xml is { } ? WsDataFormatUtils.DeserializeFromXml<XmlDocument>(item.Xml.OuterXml, Encoding.UTF8) : null;
        ProductDt = item.ProductDt;
        ExpirationDt = item.ExpirationDt;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(ProductDt)}: {ProductDt}. " +
        $"{nameof(PluScale.Plu.Number)}: {PluScale.Plu.Number}. " +
        $"{nameof(Zpl)}: {Zpl.Length}. " +
        $"{nameof(Xml)}: {(Xml is null ? 0 : Xml.OuterXml.Length)}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluLabelModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Zpl, string.Empty) &&
        Equals(Xml, null) &&
        Equals(ProductDt, DateTime.MinValue) &&
        (PluWeighing is null || PluWeighing.EqualsDefault()) &&
        PluScale.EqualsDefault();

    public override void ClearNullProperties()
    {
        if (PluWeighing is not null && PluWeighing.Identity.EqualsDefault())
            PluWeighing = null;
        //if (PluScale.Identity.EqualsDefault())
        //       PluScale = new();
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Zpl = WsLocaleCore.Sql.SqlItemFieldZpl;
        ProductDt = DateTime.Now;
        PluWeighing?.FillProperties();
        PluScale.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluLabelModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Zpl, item.Zpl) &&
        Equals(ProductDt, item.ProductDt) &&
        Equals(ExpirationDt, item.ExpirationDt) &&
        (PluWeighing is null && item.PluWeighing is null || PluWeighing is not null &&
            item.PluWeighing is not null && PluWeighing.Equals(item.PluWeighing)) &&
        (Xml is null && item.Xml is null || Xml is not null && item.Xml is not null && Xml.Equals(item.Xml)) &&
        PluScale.Equals(item.PluScale);

    #endregion
}