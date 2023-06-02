// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Section;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.ViewScaleModels;

namespace DeviceControl.Pages.Menu.Devices.Lines;

public sealed partial class Lines : SectionBase<LineView>
{
    #region Public and private fields, properties, constructor

    public Lines() : base()
    {
        SqlCrudConfigSection.AddOrders(
            new()
            {
                Name = nameof(WsSqlScaleModel.Description),
                Direction = WsSqlOrderDirection.Asc
            }
        );
    }

    #endregion

    #region Public and private

    protected override void SetSqlSectionCast()
    {
        var query = WsSqlQueriesDiags.Tables.Views.GetLines(SqlCrudConfigSection.SelectTopRowsCount
            , SqlCrudConfigSection.IsMarked);
        object[] objects = ContextManager.AccessManager.AccessList.GetArrayObjectsNotNullable(query);
        List<LineView> items = new();
        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 7 } item)
                continue;
            
            items.Add(new LineView
            {
                IdentityValueId = Convert.ToInt64(item[0]),
                IsMarked = Convert.ToBoolean(item[1]),
                Name = item[2] as string ?? string.Empty,
                Number = Convert.ToInt32(item[3]),
                HostName = item[4] as string ?? string.Empty,
                Printer = item[5] as string ?? string.Empty,
                WorkShop = item[6] as string ?? string.Empty
            });
        }

        SqlSectionCast = items;
    }

    #endregion
}