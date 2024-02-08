// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class PluEntity : Entity1CBase
{
    public virtual short Number { get; set; }
    public virtual string FullName { get; set; }
    public virtual byte ShelfLifeDays { get; set; }
    public virtual string Gtin { get; set; }
    public virtual string Ean13 { get; set; }
    public virtual string Itf14 { get; set; }
    public virtual bool IsCheckWeight { get; set; } 
    public virtual BundleEntity Bundle { get; set; }
    public virtual BrandEntity Brand { get; set; }
    public virtual ClipEntity Clip { get; set; }
    public virtual StorageMethodEntity StorageMethod { get; set; }
    public virtual string Description { get; set; } = string.Empty;
    public override string DisplayName => $"{Number} | {Name}";
    
    public PluEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Ean13 = string.Empty;
        FullName = string.Empty;
        Gtin = string.Empty;
        IsCheckWeight = false;
        Itf14 = string.Empty;
        Number = default;
        ShelfLifeDays = default;
        Brand = new();
        Bundle = new();
        Clip = new();
        StorageMethod = new();
        Description = string.Empty;
    }
    
    public override string ToString() => $"{Number} | {Name} | {Uid1C}";
    
    protected override bool CastEquals(EntityBase obj)
    {
        PluEntity item = (PluEntity)obj;
        return Equals(StorageMethod, item.StorageMethod) &&
               Equals(Number, item.Number) &&
               Equals(Clip, item.Clip) &&
               Equals(Brand, item.Brand) &&
               Equals(Bundle, item.Bundle) &&
               Equals(FullName, item.FullName) &&
               Equals(ShelfLifeDays, item.ShelfLifeDays) &&
               Equals(Gtin, item.Gtin) &&
               Equals(Ean13, item.Ean13) &&
               Equals(Itf14, item.Itf14) &&
               Equals(Description, item.Description) &&
               Equals(IsCheckWeight, item.IsCheckWeight);
    }
}