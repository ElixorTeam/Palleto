using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Features.Shared;

public class FormInputBase: ComponentBase
{
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Link { get; set; } = string.Empty;
}