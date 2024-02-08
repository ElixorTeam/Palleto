// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;

namespace Ws.Domain.Abstractions.Entities.Common;

[DebuggerDisplay("{ToString()}")]
public abstract class EntityBase
{
    public virtual IdentityModel Identity { get; set; }
    public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
    public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
    public virtual DateTime CreateDt { get; set; } = DateTime.MinValue;
    public virtual DateTime ChangeDt { get; set; } = DateTime.MinValue;
    public virtual string Name { get; set; } = string.Empty;
    public virtual bool IsExists => Identity.IsExists;
    public virtual bool IsNew => Identity.IsNew;
    public virtual string DisplayName => Name;

    public EntityBase()
    {
        Identity = new(SqlEnumFieldIdentity.Uid);
    }

    public EntityBase(SqlEnumFieldIdentity identityName) : this()
    {
        Identity = new(identityName);
    }

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        
        if (ReferenceEquals(this, obj))
            return true;
        
        EntityBase entity = (EntityBase)obj;
        
        return Identity.Equals(entity.Identity) &&
               Equals(CreateDt, entity.CreateDt) &&
               Equals(ChangeDt, entity.ChangeDt) &&
               Equals(Name, entity.Name) && 
               CastEquals(entity);
    }

    protected virtual bool CastEquals(EntityBase obj) => true;
    
    public override int GetHashCode() => Identity.GetHashCode();
}