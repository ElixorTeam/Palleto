using Blazorise;
using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Source.Shared.UI;

public sealed partial class DialogWrapper : ComponentBase
{
    [Inject] private IModalService ModalService { get; set; } = null!;

    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private async Task CloseCurrentDialog() => await ModalService.Hide();
}