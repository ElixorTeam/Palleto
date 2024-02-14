// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;

namespace Ws.Domain.Abstractions.Entities.Common;

[DebuggerDisplay("{ToString()}")]
public abstract class Entity1CBase() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual Guid Uid1C { get; set; } = Guid.Empty;

    public virtual bool Equals(Entity1CBase item) =>
        ReferenceEquals(this, item) || base.Equals(item) && Equals(Uid1C, item.Uid1C);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Entity1CBase)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
}