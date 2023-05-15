// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsSqlTableBase = WsStorageCore.Tables.WsSqlTableBase;

namespace WsStorageCore.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// Table "DEVICES_SCALES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceScaleFkModel : Tables.WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlDeviceModel Device { get; set; }
    [XmlElement] public virtual WsSqlScaleModel Scale { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceScaleFkModel() : base(WsSqlFieldIdentity.Uid)
    {
        Device = new();
        Scale = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlDeviceScaleFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Device = (WsSqlDeviceModel)info.GetValue(nameof(Device), typeof(WsSqlDeviceModel));
        Scale = (WsSqlScaleModel)info.GetValue(nameof(Scale), typeof(WsSqlScaleModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Device)}: {Device}. " +
        $"{nameof(Scale)}: {Scale}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlDeviceScaleFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Device.EqualsDefault() &&
        Scale.EqualsDefault();

    public override object Clone()
    {
        WsSqlDeviceScaleFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Device = Device.CloneCast();
        item.Scale = Scale.CloneCast();
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
        info.AddValue(nameof(Device), Device);
        info.AddValue(nameof(Scale), Scale);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Device.FillProperties();
        Scale.FillProperties();
    }

    public override void UpdateProperties(WsSqlTableBase item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures/.
        if (item is not WsSqlDeviceScaleFkModel deviceScaleFk) return;
        Device = deviceScaleFk.Device;
        Scale = deviceScaleFk.Scale;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceScaleFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Device.Equals(item.Device) &&
        Scale.Equals(item.Scale);

    public new virtual WsSqlDeviceScaleFkModel CloneCast() => (WsSqlDeviceScaleFkModel)Clone();

    #endregion
}