﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Организации".
    /// </summary>
    public class OrganizationEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDate { get; set; } = default;
        public virtual DateTime ModifiedDate { get; set; } = default;
        public virtual string Name { get; set; } = string.Empty;
        public virtual bool? Marked { get; set; }
        public virtual int Gln { get; set; }
        public virtual string SerializedRepresentationObject { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public OrganizationEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Marked)}: {Marked}. " +
                   $"{nameof(Gln)}: {Gln}. " +
                   $"{nameof(SerializedRepresentationObject)}: {SerializedRepresentationObject.Length}. ";
        }

        public virtual bool Equals(OrganizationEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(Name, entity.Name) &&
                   Equals(Marked, entity.Marked) &&
                   Equals(Gln, entity.Gln) &&
                   Equals(SerializedRepresentationObject, entity.SerializedRepresentationObject);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OrganizationEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new OrganizationEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(CreateDate, default(DateTime)) &&
                   Equals(ModifiedDate, default(DateTime)) &&
                   Equals(Name, default(string)) &&
                   Equals(Marked, default(bool?)) &&
                   Equals(Gln, default(int)) &&
                   Equals(SerializedRepresentationObject, default(string));
        }

        public override object Clone()
        {
            return new OrganizationEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                Name = Name,
                Marked = Marked,
                Gln = Gln,
                SerializedRepresentationObject = SerializedRepresentationObject,
            };
        }

        #endregion
    }
}
