using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.PlusLines;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinePluDataGrid: SectionDataGridBase<SqlPluLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    [Parameter, EditorRequired] public SqlLineEntity LineEntity { get; set; } = null!;
    
    private SqlPluLineRepository PluLineRepository { get; } = new();

    protected override void SetSqlSectionCast() =>
        SectionItems = PluLineRepository.GetListByLine(LineEntity, SqlCrudConfigSection);
    
    protected override async Task OpenItemInNewTab(SqlPluLineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Plu.IdentityValueUid}");
}