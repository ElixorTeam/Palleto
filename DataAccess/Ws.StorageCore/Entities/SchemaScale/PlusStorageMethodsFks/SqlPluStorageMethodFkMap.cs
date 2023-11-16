namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

public sealed class SqlPluStorageMethodFkMap : ClassMapping<SqlPluStorageMethodFkEntity>
{
    public SqlPluStorageMethodFkMap()
    {
        Schema(SqlSchemasUtils.DbScales);
        Table(SqlTablesUtils.PlusStorageMethodsFks);

        Id(x => x.IdentityValueUid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        ManyToOne(x => x.Plu, m =>
        {
            m.Column("PLU_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Method, m =>
        {
            m.Column("METHOD_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Resource, m =>
        {
            m.Column("RESOURCE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}