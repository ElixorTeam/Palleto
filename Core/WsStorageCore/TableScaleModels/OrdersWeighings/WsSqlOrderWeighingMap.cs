// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.OrdersWeighings;

/// <summary>
/// Table map "ORDERS_WEIGHINGS".
/// </summary>
public sealed class WsSqlOrderWeighingMap : ClassMap<WsSqlOrderWeighingModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlOrderWeighingMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.OrdersWeighings);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.Order).Column("ORDER_UID").Not.Nullable();
        References(item => item.PluWeighing).Column("PLU_WEIGHING_UID").Not.Nullable();
    }
}
