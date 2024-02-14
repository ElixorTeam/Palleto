// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class BoxEntity() : Entity1CBase
{
    public virtual decimal Weight { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    
    public override string ToString() => $"{Uid1C} | {Name} | {Weight}";

    protected override bool CastEquals(EntityBase obj)
    {
        BoxEntity item = (BoxEntity)obj;
        return Equals(Weight, item.Weight) && Equals(Name, item.Name);
    }
}