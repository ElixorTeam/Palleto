// ReSharper disable ClassNeverInstantiated.Global

using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Domain.Models.Entities.Ref1c;

namespace ScalesHybrid.Features.Shared;

public sealed partial class PluSelect : DataGridBase<PluEntity>
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;

    protected override void GetGridData() => GridData = LabelContext.PluEntities;

    protected override async Task OnItemSelect(PluEntity obj)
    {
        LabelContext.ChangePlu(obj);
        await ModalService.Hide();
    }
}