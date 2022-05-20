﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;
using System.Xml.Serialization;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "Scales".
    /// </summary>
    public class ScaleEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual TemplateEntity? TemplateDefault { get; set; }
        public virtual TemplateEntity? TemplateSeries { get; set; }
        public virtual WorkShopEntity WorkShop { get; set; }
        public virtual PrinterEntity? PrinterMain { get; set; }
        public virtual bool IsShipping { get; set; }
        public virtual bool IsKneading { get; set; }
        public virtual PrinterEntity? PrinterShipping { get; set; }
        public virtual byte ShippingLength { get; set; }
        public virtual HostEntity? Host { get; set; }
        public virtual string Description { get; set; }
        public virtual Guid IdRRef { get; set; }
        public virtual string DeviceIp { get; set; }
        public virtual short DevicePort { get; set; }
        public virtual string DeviceMac { get; set; }
        public virtual short? DeviceSendTimeout { get; set; }
        public virtual short? DeviceReceiveTimeout { get; set; }
        public virtual string DeviceComPort { get; set; }
        public virtual string ZebraIp { get; set; }
        [XmlIgnore] public virtual string ZebraLink => string.IsNullOrEmpty(ZebraIp) ? string.Empty : $"http://{ZebraIp}";
        public virtual short? ZebraPort { get; set; }
        public virtual bool IsOrder { get; set; }
        public virtual string VerScalesUi { get; set; }
        public virtual int? DeviceNumber { get; set; }
        public virtual int? ScaleFactor { get; set; }

        #endregion

        #region Constructor and destructor

        public ScaleEntity() : this(0)
        {
            //
        }

        public ScaleEntity(long id) : base(id)
        {
            TemplateDefault = new();
            TemplateSeries = new();
            WorkShop = new();
            PrinterMain = new();
            PrinterShipping = new();
            IsShipping = false;
            IsKneading = false;
            ShippingLength = 0;
            Host = new();
            Description = string.Empty;
            IdRRef = Guid.Empty;
            DeviceIp = string.Empty;
            DevicePort = 0;
            DeviceMac = string.Empty;
            DeviceSendTimeout = default;
            DeviceReceiveTimeout = default;
            DeviceComPort = string.Empty;
            ZebraIp = string.Empty;
            ZebraPort = default;
            IsOrder = false;
            VerScalesUi = string.Empty;
            DeviceNumber = default;
            ScaleFactor = default;
        }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            string strTemplateDefault = TemplateDefault != null ? TemplateDefault.IdentityId.ToString() : "null";
            string strTemplateSeries = TemplateSeries != null ? TemplateSeries.IdentityId.ToString() : "null";
            string strWorkShop = WorkShop != null ? WorkShop.IdentityId.ToString() : "null";
            string strPrinterMain = PrinterMain != null ? PrinterMain.IdentityId.ToString() : "null";
            string strPrinterShipping = PrinterShipping != null ? PrinterShipping.IdentityId.ToString() : "null";
            string strPrinterVehicle = PrinterShipping != null ? PrinterShipping.IdentityId.ToString() : "null";
            string strHost = Host != null ? Host.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Description)}: {Description}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(DeviceIp)}: {DeviceIp}. " +
                   $"{nameof(DevicePort)}: {DevicePort}. " +
                   $"{nameof(DeviceMac)}: {DeviceMac}. " +
                   $"{nameof(DeviceSendTimeout)}: {DeviceSendTimeout}. " +
                   $"{nameof(DeviceReceiveTimeout)}: {DeviceReceiveTimeout}. " +
                   $"{nameof(DeviceComPort)}: {DeviceComPort}. " +
                   $"{nameof(ZebraIp)}: {ZebraIp}. " +
                   $"{nameof(ZebraPort)}: {ZebraPort}. " +
                   $"{nameof(IsOrder)}: {IsOrder}. " +
                   $"{nameof(VerScalesUi)}: {VerScalesUi}. " +
                   $"{nameof(DeviceNumber)}: {DeviceNumber}. " +
                   $"{nameof(TemplateDefault)}: {strTemplateDefault}. " +
                   $"{nameof(TemplateSeries)}: {strTemplateSeries}. " +
                   $"{nameof(ScaleFactor)}: {ScaleFactor}. " +
                   $"{nameof(WorkShop)}: {strWorkShop}. " +
                   $"{nameof(PrinterMain)}: {strPrinterMain}. " +
                   $"{nameof(PrinterShipping)}: {strPrinterShipping}. " +
                   $"{nameof(PrinterShipping)}: {strPrinterVehicle}. " +
                   $"{nameof(IsShipping)}: {IsShipping}. " +
                   $"{nameof(IsKneading)}: {IsKneading}. " +
                   $"{nameof(ShippingLength)}: {ShippingLength}. " +
                   $"{nameof(Host)}: {strHost}. ";
        }

        public virtual bool Equals(ScaleEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            if (TemplateDefault != null && item.TemplateDefault != null && !TemplateDefault.Equals(item.TemplateDefault))
                return false;
            if (TemplateSeries != null && item.TemplateSeries != null && !TemplateSeries.Equals(item.TemplateSeries))
                return false;
            if (WorkShop != null && item.WorkShop != null && !WorkShop.Equals(item.WorkShop))
                return false;
            if (PrinterMain != null && item.PrinterMain != null && !PrinterMain.Equals(item.PrinterMain))
                return false;
            if (PrinterShipping != null && item.PrinterShipping != null && !PrinterShipping.Equals(item.PrinterShipping))
                return false;
            if (Host != null && item.Host != null && !Host.Equals(item.Host))
                return false;
            return base.Equals(item) &&
                   Equals(Description, item.Description) &&
                   Equals(IdRRef, item.IdRRef) &&
                   Equals(DeviceIp, item.DeviceIp) &&
                   Equals(DevicePort, item.DevicePort) &&
                   Equals(DeviceMac, item.DeviceMac) &&
                   Equals(DeviceSendTimeout, item.DeviceSendTimeout) &&
                   Equals(DeviceReceiveTimeout, item.DeviceReceiveTimeout) &&
                   Equals(DeviceComPort, item.DeviceComPort) &&
                   Equals(ZebraIp, item.ZebraIp) &&
                   Equals(ZebraPort, item.ZebraPort) &&
                   Equals(IsOrder, item.IsOrder) &&
                   Equals(VerScalesUi, item.VerScalesUi) &&
                   Equals(DeviceNumber, item.DeviceNumber) &&
                   Equals(ScaleFactor, item.ScaleFactor) &&
                   IsShipping.Equals(item.IsShipping) &&
                   IsKneading.Equals(item.IsKneading) &&
                   ShippingLength.Equals(item.ShippingLength);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ScaleEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ScaleEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (TemplateDefault != null && !TemplateDefault.EqualsDefault())
                return false;
            if (TemplateSeries != null && !TemplateSeries.EqualsDefault())
                return false;
            if (WorkShop != null && !WorkShop.EqualsDefault())
                return false;
            if (PrinterMain != null && !PrinterMain.EqualsDefault())
                return false;
            if (PrinterShipping != null && !PrinterShipping.EqualsDefault())
                return false;
            if (Host != null && !Host.EqualsDefault())
                return false;
            return base.EqualsDefault(IdentityName) &&
                   Equals(Description, string.Empty) &&
                   Equals(IdRRef, Guid.Empty) &&
                   Equals(DeviceIp, string.Empty) &&
                   Equals(DevicePort, (short)0) &&
                   Equals(DeviceMac, string.Empty) &&
                   Equals(DeviceSendTimeout, null) &&
                   Equals(DeviceReceiveTimeout, null) &&
                   Equals(DeviceComPort, string.Empty) &&

                   Equals(ZebraIp, string.Empty) &&
                   Equals(ZebraPort, null) &&
                   Equals(IsOrder, false) &&
                   Equals(VerScalesUi, string.Empty) &&

                   Equals(DeviceNumber, null) &&
                   Equals(ScaleFactor, null) &&
                   Equals(IsShipping, false) &&
                   Equals(IsKneading, false) &&
                   Equals(ShippingLength, (byte)0);
        }

        public new virtual object Clone()
        {
            ScaleEntity item = new();
            item.TemplateDefault = TemplateDefault?.CloneCast();
            item.TemplateSeries = TemplateSeries?.CloneCast();
            item.WorkShop = WorkShop.CloneCast();
            item.PrinterMain = PrinterMain?.CloneCast();
            item.PrinterShipping = PrinterShipping?.CloneCast();
            item.IsShipping = IsShipping;
            item.IsKneading = IsKneading;
            item.ShippingLength = ShippingLength;
            item.Host = Host?.CloneCast();
            item.Description = Description;
            item.IdRRef = IdRRef;
            item.DeviceIp = DeviceIp;
            item.DevicePort = DevicePort;
            item.DeviceMac = DeviceMac;
            item.DeviceSendTimeout = DeviceSendTimeout;
            item.DeviceReceiveTimeout = DeviceReceiveTimeout;
            item.DeviceComPort = DeviceComPort;
            item.ZebraIp = ZebraIp;
            item.ZebraPort = ZebraPort;
            item.IsOrder = IsOrder;
            item.VerScalesUi = VerScalesUi;
            item.DeviceNumber = DeviceNumber;
            item.ScaleFactor = ScaleFactor;
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual ScaleEntity CloneCast() => (ScaleEntity)Clone();

        #endregion
    }
}