﻿namespace WsStorageCore.Entities.SchemaRef.Printers;

public class WsSqlPrinterRepository : WsSqlTableRepositoryBase<WsSqlPrinterEntity>
{
    public IEnumerable<WsSqlPrinterEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlPrinterEntity>(sqlCrudConfig);
    }

}