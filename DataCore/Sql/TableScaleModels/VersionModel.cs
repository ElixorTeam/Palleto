﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "VERSIONS".
/// </summary>
[Serializable]
public class VersionModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    public virtual DateTime ReleaseDt { get; set; }
    public virtual short Version { get; set; }
    public virtual string Description { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public VersionModel()
	{
		ReleaseDt = DateTime.MinValue;
		Version = 0;
		Description = string.Empty;
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected VersionModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ReleaseDt = info.GetDateTime(nameof(ReleaseDt));
        Version = info.GetInt16(nameof(Version));
        Description = info.GetString(nameof(Description));
    }

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(ReleaseDt)}: {ReleaseDt}. " +
        $"{nameof(Version)}: {Version}. " +
        $"{nameof(Description)}: {Description}. ";

    public virtual bool Equals(VersionModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return 
	        base.Equals(item) &&
            Equals(ReleaseDt, item.ReleaseDt) &&
            Equals(Version, item.Version) &&
            Equals(Description, item.Description);
    }

	public new virtual bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((VersionModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return 
	        base.EqualsDefault() &&
            Equals(ReleaseDt, DateTime.MinValue) &&
            Equals(Version, (short)0) &&
            Equals(Description, string.Empty);
    }

    public new virtual int GetHashCode() => base.GetHashCode();

	public new virtual object Clone()
    {
        VersionModel item = new();
        item.ReleaseDt = ReleaseDt;
        item.Version = Version;
        item.Description = Description;
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public new virtual VersionModel CloneCast() => (VersionModel)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(ReleaseDt), ReleaseDt);
        info.AddValue(nameof(Version), Version);
        info.AddValue(nameof(Description), Description);
    }

    #endregion
}