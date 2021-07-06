﻿using System;

namespace DeviceControl.Core.DAL.TableModels
{
    public class WeithingFactSummaryEntity
    {
        #region Public and private fields and properties

        public virtual DateTime WeithingDate { get; set; }
        public virtual int Count { get; set; }
        public virtual string Scale { get; set; }
        public virtual string Host { get; set; }
        public virtual string Printer { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(WeithingDate)}: {WeithingDate}. " +
                   $"{nameof(Count)}: {Count}. " +
                   $"{nameof(Scale)}: {Scale}. " +
                   $"{nameof(Host)}: {Host}. " +
                   $"{nameof(Printer)}: {Printer}. ";
        }

        public virtual bool Equals(WeithingFactSummaryEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(WeithingDate, entity.WeithingDate) &&
                   Equals(Count, entity.Count) &&
                   Equals(Scale, entity.Scale) &&
                   Equals(Host, entity.Host) &&
                   Equals(Printer, entity.Printer);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WeithingFactSummaryEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new WeithingFactSummaryEntity());
        }

        public virtual bool EqualsDefault()
        {
            return 
                   Equals(WeithingDate, default(DateTime)) &&
                   Equals(Count, default(int)) &&
                   Equals(Scale, default(string)) &&
                   Equals(Host, default(string)) &&
                   Equals(Printer, default(string));
        }

        public object Clone()
        {
            return new WeithingFactSummaryEntity
            {
                WeithingDate = WeithingDate,
                Count = Count,
                Scale = Scale,
                Host = Host,
                Printer = Printer,
            };
        }

        #endregion
    }
}
