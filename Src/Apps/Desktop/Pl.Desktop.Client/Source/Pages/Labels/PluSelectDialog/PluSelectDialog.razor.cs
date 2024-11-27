using Pl.Desktop.Models.Features.Plus.Weight.Output;

namespace Pl.Desktop.Client.Source.Pages.Labels.PluSelectDialog;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class PluSelectDialog : ComponentBase, IDialogContentComponent<PluDialogContent>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Parameter] public PluDialogContent Content { get; set; } = default!;
    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;
}

public record PluDialogContent
{
    public IQueryable<PluWeightDto> Data { get; init; } = new List<PluWeightDto>().AsQueryable();
}