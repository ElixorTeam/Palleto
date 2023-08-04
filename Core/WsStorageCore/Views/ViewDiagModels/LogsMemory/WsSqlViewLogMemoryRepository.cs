// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewDiagModels.LogsMemory;

public sealed class WsSqlViewLogMemoryRepository : IViewLogMemoryRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    // Если оставить прокси как есть, то будет падать, т.к. для вьюшки нет маппингов!
    public List<WsSqlViewLogMemoryModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
        => GetList(sqlCrudConfig.SelectTopRowsCount);

    public List<WsSqlViewLogMemoryModel> GetList(int topRecords = 0, string appName = "")
    {
        List<WsSqlViewLogMemoryModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewLogsMemory(topRecords, appName);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 7) break;
            result.Add(new()
            {
                IdentityValueUid = Guid.Parse(Convert.ToString(item[i++])),
                CreateDt = Convert.ToDateTime(item[i++]),
                AppName = Convert.ToString(item[i++]),
                DeviceName = Convert.ToString(item[i++]),
                ScaleName = Convert.ToString(item[i++]),
                SizeAppMb = Convert.ToInt16(item[i++]),
                SizeFreeMb = Convert.ToInt16(item[i++])
            });
        }
        return result;
    }
}