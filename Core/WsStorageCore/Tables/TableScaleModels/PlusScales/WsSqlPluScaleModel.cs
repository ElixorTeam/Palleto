// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.Tables.TableScaleModels.PlusScales;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPluScaleModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual bool IsActive { get; set; }
    public virtual WsSqlPluModel Plu { get; set; }
    public virtual WsSqlScaleModel Line { get; set; }
    
    public WsSqlPluScaleModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        IsActive = false;
        Plu = new();
        Line = new();
    }
    
    public WsSqlPluScaleModel(WsSqlPluScaleModel item) : base(item)
    {
        IsActive = item.IsActive;
        Plu = new(item.Plu);
        Line = new(item.Line);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {IsActive} | {Plu} | {Line}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluScaleModel)obj);
    }

    public override int GetHashCode() => (IsActive, Plu, Scale: Line).GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsActive, false) &&
        Plu.EqualsDefault() &&
        Line.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        IsActive = true;
        Plu.FillProperties();
        Line.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluScaleModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(IsActive, item.IsActive) &&
        Equals(Plu, item.Plu) &&
        Equals(Line, item.Line) &&
        Plu.Equals(item.Plu) &&
        Line.Equals(item.Line);

    #endregion
}
