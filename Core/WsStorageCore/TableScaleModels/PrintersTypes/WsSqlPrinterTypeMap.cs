// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PrintersTypes;

/// <summary>
/// Table map "ZebraPrinterType".
/// </summary>
public sealed class WsSqlPrinterTypeMap : ClassMap<WsSqlPrinterTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPrinterTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PrintersTypes);
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(100).Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
    }
}