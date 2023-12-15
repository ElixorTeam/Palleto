using DeviceControl.Features.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesDialog: SectionDialogBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        TabsList = new()
        {
            new(Localizer["SectionLines"], "main"),
            new(Localizer["SectionLinePLU"], "plu")
        };
    }
}