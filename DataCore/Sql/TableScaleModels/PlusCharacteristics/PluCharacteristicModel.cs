﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.PlusCharacteristics;

/// <summary>
/// Table "NOMENCLATURES_CHARACTERISTICS".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluCharacteristicModel)} | {nameof(Uid1C)} = {Uid1C} | {AttachmentsCount}")]
public class PluCharacteristicModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual decimal AttachmentsCount { get; set; }
    [XmlIgnore] public virtual Guid Uid1C { get; set; }

    public PluCharacteristicModel() : base(SqlFieldIdentity.Uid)
    {
        AttachmentsCount = 0;
        Uid1C = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluCharacteristicModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        AttachmentsCount = info.GetDecimal(nameof(AttachmentsCount));
        Uid1C = info.GetValue(nameof(Uid1C), typeof(Guid)) is Guid uid1C ? uid1C : Guid.Empty;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(AttachmentsCount)}: {AttachmentsCount}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluCharacteristicModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(AttachmentsCount, (decimal)0) &&
        Equals(Uid1C, Guid.Empty);

    public override object Clone()
    {
        PluCharacteristicModel item = new();
        item.CloneSetup(base.CloneCast());
        item.AttachmentsCount = AttachmentsCount;
        item.Uid1C = Uid1C;
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
        info.AddValue(nameof(AttachmentsCount), AttachmentsCount);
        info.AddValue(nameof(Uid1C), Uid1C);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluCharacteristicModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(AttachmentsCount, item.AttachmentsCount);
    public new virtual PluCharacteristicModel CloneCast() => (PluCharacteristicModel)Clone();

    #endregion
}