﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "DEVICES_TYPES".
/// </summary>
[Serializable]
public class DeviceTypeModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string PrettyName { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public DeviceTypeModel() : base(SqlFieldIdentityEnum.Uid)
	{
        PrettyName = string.Empty;
	}

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
	/// <param name="context"></param>
	private DeviceTypeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PrettyName = info.GetString(nameof(PrettyName));
    }

	#endregion

	#region Public and private methods - override

	/// <summary>
	/// To string.
	/// </summary>
	/// <returns></returns>
	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((DeviceTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
	    base.EqualsDefault() &&
		Equals(PrettyName, string.Empty);
    
    public override object Clone()
    {
        DeviceTypeModel item = new();
        item.PrettyName = PrettyName;
		item.CloneSetup(base.CloneCast());
		return item;
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
		info.AddValue(nameof(PrettyName), PrettyName);
	}

    public override void FillProperties()
    {
		base.FillProperties();
		PrettyName = LocaleCore.Sql.SqlItemFieldPrettyName;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(DeviceTypeModel item) =>
	    ReferenceEquals(this, item) || base.Equals(item) && //-V3130
	    Equals(PrettyName, item.PrettyName);

    public new virtual DeviceTypeModel CloneCast() => (DeviceTypeModel)Clone();

	#endregion
}