// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.LogsTypes;

/// <summary>
/// Table map "LOG_TYPES".
/// </summary>
public sealed class WsSqlLogTypeMap : ClassMap<WsSqlLogTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlLogTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.LogsTypes);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Number).CustomSqlType("TINYINT").Column("NUMBER").Not.Nullable();
        Map(x => x.Icon).CustomSqlType("NVARCHAR").Column("ICON").Length(32).Not.Nullable();
    }
}