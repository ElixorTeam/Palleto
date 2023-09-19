namespace WsStorageCore.Tables.TableScaleModels.PrintersTypes;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPrinterTypeModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    
    public WsSqlPrinterTypeModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        //
    }

    public WsSqlPrinterTypeModel(WsSqlPrinterTypeModel item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPrinterTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPrinterTypeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}
