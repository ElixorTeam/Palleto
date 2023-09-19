namespace WsStorageCore.Tables.TableScaleModels.Templates;

/// <summary>
/// Table "Templates".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlTemplateModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual string CategoryId { get; set; } 
    public virtual string Title { get; set; }
    public virtual string Data { get; set; }
    
    public WsSqlTemplateModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        CategoryId = string.Empty;
        Title = string.Empty;
        Data = string.Empty;
    }

    public WsSqlTemplateModel(WsSqlTemplateModel item) : base(item)
    {
        CategoryId = item.CategoryId;
        Title = item.Title;
        Data = item.Data;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {CategoryId} | {Title}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTemplateModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(CategoryId, string.Empty) &&
        Equals(Title, string.Empty) &&
        Equals(Data, string.Empty);

    public override void FillProperties()
    {
        base.FillProperties();
        Data = WsLocaleCore.Sql.SqlItemFieldTemplateData;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlTemplateModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(CategoryId, item.CategoryId) &&
        Equals(Title, item.Title) &&
        Equals(Data, item.Data);

    #endregion
}