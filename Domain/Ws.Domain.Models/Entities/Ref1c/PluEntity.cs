// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class PluEntity : Table1CBase
{
    public virtual bool IsGroup { get; set; }
    public virtual short Number { get; set; }
    public virtual string Code { get; set; }
    public virtual string FullName { get; set; }
    public virtual byte ShelfLifeDays { get; set; }
    public virtual string Gtin { get; set; }
    public virtual string Ean13 { get; set; }
    public virtual string Itf14 { get; set; }
    public virtual bool IsCheckWeight { get; set; } 
    public virtual Guid ParentGuid { get; set; }
    public virtual Guid CategoryGuid { get; set; }
    public virtual BundleEntity Bundle { get; set; }
    public virtual BrandEntity Brand { get; set; }
    public virtual StorageMethodEntity StorageMethod { get; set; }
    public virtual string Description { get; set; } = string.Empty;
    public override string DisplayName => $"{Number} | {Name}";


    public PluEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        CategoryGuid = Guid.Empty;
        Code = string.Empty;
        Ean13 = string.Empty;
        FullName = string.Empty;
        Gtin = string.Empty;
        IsCheckWeight = false;
        IsGroup = default;
        Itf14 = string.Empty;
        Number = default;
        ParentGuid = Guid.Empty;
        ShelfLifeDays = default;
        Brand = new();
        Bundle = new();
        StorageMethod = new();
        Description = string.Empty;
    }

    public PluEntity(PluEntity item) : base(item)
    {
        IsGroup = item.IsGroup;
        ParentGuid = item.ParentGuid;
        Code = item.Code;
        Number = item.Number;
        FullName = item.FullName;
        ShelfLifeDays = item.ShelfLifeDays;
        Gtin = item.Gtin;
        Ean13 = item.Ean13;
        Itf14 = item.Itf14;
        IsCheckWeight = item.IsCheckWeight;
        Brand = new(item.Brand);
        Bundle = new(item.Bundle);
        StorageMethod = new(item.StorageMethod);
        Description = item.Description;
    }

    public override string ToString() => $"{Number} | {Name} | {Uid1C} | {Code}";
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(PluEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(IsGroup, item.IsGroup) &&
        Equals(ParentGuid, item.ParentGuid) &&
        Equals(Code, item.Code) &&
        Equals(StorageMethod, item.StorageMethod) &&
        Equals(Number, item.Number) &&
        Equals(FullName, item.FullName) &&
        Equals(ShelfLifeDays, item.ShelfLifeDays) &&
        Equals(Gtin, item.Gtin) &&
        Equals(Ean13, item.Ean13) &&
        Equals(Itf14, item.Itf14) &&
        Equals(Description, item.Description) &&
        Equals(IsCheckWeight, item.IsCheckWeight);
}