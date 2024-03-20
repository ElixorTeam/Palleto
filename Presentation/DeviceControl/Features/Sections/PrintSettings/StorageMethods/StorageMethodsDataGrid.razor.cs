using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.StorageMethod;

namespace DeviceControl.Features.Sections.PrintSettings.StorageMethods;

public sealed partial class StorageMethodsDataGrid : SectionDataGridBase<StorageMethodEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IStorageMethodService StorageMethodService { get; set; } = null!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<StorageMethodsCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(StorageMethodEntity item)
        => await OpenSectionModal<StorageMethodsUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(StorageMethodEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionStorageMethods}/{item.Uid.ToString()}");

    protected override IEnumerable<StorageMethodEntity> SetSqlSectionCast() => StorageMethodService.GetAll();

    protected override IEnumerable<StorageMethodEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [StorageMethodService.GetItemByUid(itemUid)];
    }
}