namespace WsStorageCore.Tables.TableRef1cModels.Brands;

[DebuggerDisplay("{ToString()}")]
public class WsSqlBrandModel : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    public virtual string Code { get; set; }
    
    public WsSqlBrandModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Code = string.Empty;
    }
    
    public WsSqlBrandModel(WsSqlBrandModel item) : base(item)
    {
        Code = item.Code;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Code)}: {Code}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlBrandModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && Equals(Code, string.Empty);

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlBrandModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Code, item.Code);
    
    #endregion
}