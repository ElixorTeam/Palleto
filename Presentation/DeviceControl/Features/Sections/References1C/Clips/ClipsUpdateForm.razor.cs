using DeviceControl.Features.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References1C.Clips;


public sealed partial class ClipsUpdateForm: SectionFormBase<SqlClipEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}
