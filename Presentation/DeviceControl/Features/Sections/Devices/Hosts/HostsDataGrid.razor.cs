using Blazorise;
using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Features.Sections.Devices.Hosts;

public sealed partial class HostsDataGrid: SectionDataGridBase<SqlHostEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlHostRepository HostRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(SqlHostEntity item)
        => await OpenSectionModal<HostsUpdateDialog>(item);
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<HostsCreateDialog>(new());

    protected override void SetSqlSectionCast() =>
        SectionItems = HostRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { HostRepository.GetItemByUid(itemUid) };
    }
}