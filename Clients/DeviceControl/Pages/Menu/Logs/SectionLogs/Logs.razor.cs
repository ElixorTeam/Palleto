// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableDiagModels.LogsTypes;
using WsStorageCore.Xml;

namespace BlazorDeviceControl.Pages.Menu.Logs.SectionLogs;

public sealed partial class Logs : RazorComponentSectionBase<LogQuickModel>
{
    private static LogTypeModel LogTypeNew => new();
    private LogTypeModel CurrentType { get; set; }
    private List<LogTypeModel> LogTypes { get; set; }

    public Logs() : base()
    {
        CurrentType = new();
        LogTypes = ContextManager.AccessManager.AccessList.GetListNotNullable<LogTypeModel>(new SqlCrudConfigModel());
        SqlCrudConfigSection.IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultShowMarked = true;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = new(false, false,  false, false, false, false, false);
    }

    protected override void SetSqlSectionCast()
    {
        var logTypeUid = CurrentType.IdentityValueUid;
        var query = WsSqlQueriesDiags.Tables.GetLogs(SqlCrudConfigSection.IsResultShowOnlyTop
            ? ContextManager.JsonSettings.Local.SelectTopRowsCount
            : 0, SqlCrudConfigSection.IsResultShowMarked, logTypeUid);
        object[] objects = ContextManager.AccessManager.AccessList.GetArrayObjectsNotNullable(query);
        List<LogQuickModel> items = new();
        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 12 } item)
                continue;

            if (Guid.TryParse(Convert.ToString(item[0]), out var uid))
            {
                items.Add(new LogQuickModel
                {
                    IdentityValueUid = uid,
                    CreateDt = Convert.ToDateTime(item[1]),
                    Scale = item[2] as string ?? string.Empty,
                    Host = item[4] as string ?? string.Empty,
                    App = item[5] as string ?? string.Empty,
                    Version = item[6] as string ?? string.Empty,
                    File = item[7] as string ?? string.Empty,
                    Line = Convert.ToInt32(item[8]),
                    Member = item[9] as string ?? string.Empty,
                    Icon = item[10] as string ?? string.Empty,
                    Message = item[11] as string ?? string.Empty
                });
            }
        }

        SqlSectionCast = items;
    }

    private void OnSelectTypeChanged(LogTypeModel logType)
    {
        CurrentType = logType;
        SqlCrudConfigSection.IsResultShowOnlyTop = true;
        GetSectionData();
    }
}