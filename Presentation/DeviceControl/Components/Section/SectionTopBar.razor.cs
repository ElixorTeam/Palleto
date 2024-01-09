namespace DeviceControl.Components.Section;

public partial class SectionTopBar : ComponentBase
{
    [Parameter] public string Title { get; set; }
    [Parameter] public SqlCrudConfigModel SqlCrudConfigSection { get; set; }
    [Parameter] public ButtonSettingsModel ButtonSettings { get; set; }
    [Parameter] public EventCallback<bool> OnSectionUpdate { get; set; }
    [Parameter] public EventCallback OnSectionAdd { get; set; }
    [Parameter] public int SectionCount { get; set; }
    [Parameter] public bool IsGuiShowFilterMarked { get; set; }
    private string SqlListCountResult => $"{Locale.ItemsCount}: {SectionCount:### ### ###}";

    private List<int> _rowCountList = new() { 0, 200, 400, 600, 800, 1000 };
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
            return;
        if (_rowCountList.Contains(SqlCrudConfigSection.SelectTopRowsCount))
            return;
        _rowCountList.Add(SqlCrudConfigSection.SelectTopRowsCount);
        _rowCountList.Sort();
    }

    private void OnItemsCountSelectUpdate()
    {
        OnSectionUpdate.InvokeAsync(false);
    }

    private void OnReload()
    {
        OnSectionUpdate.InvokeAsync(true);
    }
}
