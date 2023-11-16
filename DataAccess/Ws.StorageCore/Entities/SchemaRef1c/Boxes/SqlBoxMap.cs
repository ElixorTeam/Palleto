namespace Ws.StorageCore.Entities.SchemaRef1c.Boxes;

/// <summary>
/// Table map "BOXES".
/// </summary>
public class SqlBoxMap : ClassMapping<SqlBoxEntity>
{
    public SqlBoxMap()
    {
        Schema(SqlSchemasUtils.DbScales);
        Table(SqlTablesUtils.Boxes);

        Id(x => x.IdentityValueUid, m => {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.IsMarked, m => {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Name, m => {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.Unique(true);
            m.NotNullable(true);
        });

        Property(x => x.Uid1C, m => {
            m.Column("UID_1C");
            m.Type(NHibernateUtil.Guid);
            m.NotNullable(true);
        });

        Property(x => x.Weight, m => {
            m.Column("WEIGHT");
            m.Type(NHibernateUtil.Decimal);
            m.Precision(10);
            m.Scale(3);
            m.NotNullable(true);
        });

        Property(x => x.CreateDt, m => {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.ChangeDt, m => {
            m.Column("CHANGE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

    }
}