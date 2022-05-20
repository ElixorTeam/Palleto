﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "ProductionFacilities".
    /// </summary>
    public class ProductionFacilityEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual Guid IdRRef { get; set; }

        #endregion

        #region Constructor and destructor

        public ProductionFacilityEntity() : this(0)
        {
            //
        }

        public ProductionFacilityEntity(long id) : base(id)
        {
            Name = string.Empty;
            IdRRef = Guid.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            base.ToString() +
            $"{nameof(Name)}: {Name}. " +
            $"{nameof(IdRRef)}: {IdRRef}. ";

        public virtual bool Equals(ProductionFacilityEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(Name, item.Name) &&
                   Equals(IdRRef, item.IdRRef);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ProductionFacilityEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ProductionFacilityEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault(IdentityName) &&
                   Equals(Name, string.Empty) &&
                   Equals(IdRRef, Guid.Empty);
        }

        public new virtual object Clone()
        {
            ProductionFacilityEntity item = new();
            item.Name = Name;
            item.IdRRef = IdRRef;
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual ProductionFacilityEntity CloneCast() => (ProductionFacilityEntity)Clone();

        #endregion
    }
}