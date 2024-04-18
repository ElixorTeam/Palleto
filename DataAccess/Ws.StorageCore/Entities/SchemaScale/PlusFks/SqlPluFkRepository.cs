namespace Ws.StorageCore.Entities.SchemaScale.PlusFks;

public sealed class SqlPluFkRepository : SqlTableRepositoryBase<SqlPluFkEntity>
{
    public IEnumerable<SqlPluFkEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlPluFkEntity> items = SqlCore.GetEnumerable<SqlPluFkEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }
    
    public SqlPluFkEntity GetByPlu(SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<SqlPluFkEntity>(sqlCrudConfig);
    }
}