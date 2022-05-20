﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Utils;

namespace DataCore.Sql.TableDwhModels
{
    public class NomenclatureTypeEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual bool GoodsForSale { get; set; }
        public virtual int StatusId { get; set; }
        public virtual InformationSystemEntity InformationSystem { get; set; }
        public virtual byte[] CodeInIs { get; set; }

        #endregion

        #region Constructor and destructor

        public NomenclatureTypeEntity() : this(0)
        {
            //
        }

        public NomenclatureTypeEntity(long id) : base(id)
        {
            Name = string.Empty;
            GoodsForSale = false;
            StatusId = 0;
            InformationSystem = new();
            CodeInIs = new byte[0];
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strInformationSystem = InformationSystem != null ? InformationSystem.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(GoodsForSale)}: {GoodsForSale}. " +
                   $"{nameof(StatusId)}: {StatusId}. " +
                   $"{nameof(InformationSystem)}: {strInformationSystem}. " +
                   $"{nameof(CodeInIs)}.Length: {CodeInIs?.Length ?? 0}. ";
        }

        public virtual bool Equals(NomenclatureTypeEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            if (InformationSystem != null && item.InformationSystem != null && !InformationSystem.Equals(item.InformationSystem))
                return false;
            return base.Equals(item) &&
                   Equals(Name, item.Name) &&
                   Equals(GoodsForSale, item.GoodsForSale) &&
                   Equals(StatusId, item.StatusId) &&
                   Equals(CodeInIs, item.CodeInIs);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NomenclatureTypeEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new NomenclatureTypeEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (InformationSystem != null && !InformationSystem.EqualsDefault())
                return false;
            return base.EqualsDefault(IdentityName) &&
                   Equals(Name, string.Empty) &&
                   Equals(GoodsForSale, false) &&
                   Equals(StatusId, 0) &&
                   Equals(CodeInIs, new byte[0]);
        }

        public new virtual object Clone()
        {
            NomenclatureTypeEntity item = new();
            item.Name = Name;
            item.GoodsForSale = GoodsForSale;
            item.StatusId = StatusId;
            item.InformationSystem = InformationSystem.CloneCast();
            item.CodeInIs = DataUtils.ByteClone(CodeInIs);
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual NomenclatureTypeEntity CloneCast() => (NomenclatureTypeEntity)Clone();

        #endregion
    }
}