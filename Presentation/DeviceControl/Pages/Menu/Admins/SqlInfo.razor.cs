namespace DeviceControl.Pages.Menu.Admins;

public sealed partial class SqlInfo : ComponentBase
{
    #region Public and private fields, properties, constructor
    private static WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;
    private WsSqlViewTableSizeRepository WsSqlViewTableSizeRepository { get; } = new();
    private List<WsSqlViewDbFileSizeInfoModel> DbFiles { get; set; }
    private List<WsSqlViewTableSizeModel> DbTables { get; set; }
    
    private static string SqlConnectionString => 
        $"{JsonSettings.Local.Sql.DataSource} \\ " +
        $"{JsonSettings.Local.Sql.InitialCatalog} \\ " +
        $"{JsonSettings.Local.Sql.UserId}";
    
    public SqlInfo()
    {
        DbFiles = new();
        DbTables = new();
    }
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
        {
            base.OnAfterRender(firstRender);
            return;
        }
        GetSectionData();
    }

    private void GetSectionData()
    {
        DbFiles = new WsSqlViewDbFileSizeRepository().GetList();
        DbTables = WsSqlViewTableSizeRepository.GetEnumerable(new()).ToList();
        foreach (WsSqlViewDbFileSizeInfoModel dbFile in DbFiles)
        {
            dbFile.Tables.AddRange(DbTables.Where(table => table.FileName == dbFile.FileName));
        }
        StateHasChanged();
    }
    
    private static void RowRender(RowRenderEventArgs<WsSqlViewDbFileSizeInfoModel> args)
    {
        args.Expandable = args.Data.Tables.Count > 0;
    }
    

    #endregion
}
