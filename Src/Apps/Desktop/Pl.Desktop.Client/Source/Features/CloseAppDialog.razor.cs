using Pl.Desktop.Client.Source.Shared.Localization;

namespace Pl.Desktop.Client.Source.Features;

public sealed partial class CloseAppDialog : ComponentBase, IDialogContentComponent
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;

    private static void ExitApp() => MauiWinUIApplication.Current.Exit();
    private async Task CloseDialog() => await Dialog.CloseAsync();
}