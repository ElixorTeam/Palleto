﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "BarCodeTypes".
    /// </summary>
    [Obsolete(@"Use BarcodeTypeEntityV2")]
    public class BarcodeTypeEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }

        #endregion

        #region Constructor and destructor

        public BarcodeTypeEntity() : this(0)
        {
            //
        }

        public BarcodeTypeEntity(long id) : base(id)
        {
            Name = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            base.ToString() +
            $"{nameof(Name)}: {Name}. ";

        public virtual bool Equals(BarcodeTypeEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(Name, item.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BarcodeTypeEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new BarcodeTypeEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault(IdentityName) &&
            Equals(Name, string.Empty);
        }

        public new virtual object Clone()
        {
            BarcodeTypeEntity item = new();
            item.Name = Name;
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual BarcodeTypeEntity CloneCast() => (BarcodeTypeEntity)Clone();

        #endregion
    }
}