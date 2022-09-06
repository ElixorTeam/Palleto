﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableDwhModels;

[Serializable]
public class InformationSystemModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    public virtual string Name { get; set; }
    public virtual string ConnectString1 { get; set; }
    public virtual string ConnectString2 { get; set; }
    public virtual string ConnectString3 { get; set; }
    public virtual int StatusId { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public InformationSystemModel() : base(ColumnName.Id)
    {
	    Name = string.Empty;
	    ConnectString1 = string.Empty;
	    ConnectString2 = string.Empty;
	    ConnectString3 = string.Empty;
	    StatusId = 0;
    }

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(StatusId)}: {StatusId}. ";

    public virtual bool Equals(InformationSystemModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return 
	        base.Equals(item) &&
            Equals(Name, item.Name) &&
            Equals(ConnectString1, item.ConnectString1) &&
            Equals(ConnectString2, item.ConnectString2) &&
            Equals(ConnectString3, item.ConnectString3) &&
            Equals(StatusId, item.StatusId);
    }

	public new virtual bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((InformationSystemModel)obj);
    }

    public new virtual int GetHashCode() => base.GetHashCode();

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return 
	        base.EqualsDefault() &&
            Equals(Name, string.Empty) &&
            Equals(ConnectString1, string.Empty) &&
            Equals(ConnectString2, string.Empty) &&
            Equals(ConnectString3, string.Empty) &&
            Equals(StatusId, 0);
    }

    public new virtual object Clone()
    {
        InformationSystemModel item = new();
        item.Name = Name;
        item.ConnectString1 = ConnectString1;
        item.ConnectString2 = ConnectString2;
        item.ConnectString3 = ConnectString3;
        item.StatusId = StatusId;
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public new virtual InformationSystemModel CloneCast() => (InformationSystemModel)Clone();

    #endregion
}