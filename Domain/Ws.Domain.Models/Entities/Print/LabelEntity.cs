// ReSharper disable VirtualMemberCallInConstructor
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Utils;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class LabelEntity : EntityBase
{
    #region Public and private fields, properties, constructor
    public virtual string Zpl { get; set; }
    public virtual string BarcodeTop { get; set; }
    public virtual string BarcodeRight { get; set; }
    public virtual string BarcodeBottom { get; set; }
    public virtual decimal WeightNet { get; set; }
    public virtual decimal WeightTare { get; set; }
    public virtual short Kneading { get; set; }
    public virtual PalletEntity? Pallet { get; set; }
    public virtual PluEntity Plu { get; set; }
    public virtual LineEntity Line { get; set; }
    public virtual DateTime ProductDt { get; set; }
    public virtual DateTime ExpirationDt { get; set; }
    
    public LabelEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Pallet = null;

        Plu = new();
        Line = new();
        Zpl = string.Empty;
        Kneading = 0;
        
        ProductDt = SqlTypeUtils.MinDateTime;
        ExpirationDt = SqlTypeUtils.MinDateTime;
        
        BarcodeRight = string.Empty;
        BarcodeBottom = string.Empty;
        BarcodeTop = string.Empty;
    }

    public LabelEntity(LabelEntity item) : base(item)
    {
        Zpl = item.Zpl;
        BarcodeTop = item.BarcodeTop;
        BarcodeBottom = item.BarcodeBottom;
        BarcodeRight = item.BarcodeRight;
        WeightNet = item.WeightNet;
        WeightTare = item.WeightTare;
        Kneading = item.Kneading;
        ProductDt = item.ProductDt;
        ExpirationDt = item.ExpirationDt;
        Plu = new(item.Plu);
        Line = new(item.Line);
        if (item.Pallet != null) Pallet = new(item.Pallet);
    }

    #endregion

    #region Public and private methods - override
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LabelEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => $"{CreateDt} : {Plu.Name}";

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(LabelEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Zpl, item.Zpl) &&
        Equals(BarcodeTop, item.BarcodeTop) &&
        Equals(BarcodeRight, item.BarcodeRight) &&
        Equals(BarcodeBottom, item.BarcodeBottom) &&
        Equals(WeightNet, item.WeightNet) &&
        Equals(WeightTare, item.WeightTare) &&
        Equals(ProductDt, item.ProductDt) &&
        Equals(ExpirationDt, item.ExpirationDt) &&
        Equals(Kneading, item.Kneading) &&
        Equals(Plu, item.Plu) &&
        Equals(Line, item.Line) &&
        Equals(Pallet, item.Pallet);

    #endregion
}