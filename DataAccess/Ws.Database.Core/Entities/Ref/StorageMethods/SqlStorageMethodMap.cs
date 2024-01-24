using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.StorageMethods;

internal sealed class SqlStorageMethodMap : ClassMapping<StorageMethodEntity>
{
    public SqlStorageMethodMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.StorageMethods);

        Id(x => x.IdentityValueUid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(32);
            m.NotNullable(true);
        });

        Property(x => x.Zpl, m =>
        {
            m.Column("ZPL");
            m.Length(1024);
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });
    }
}