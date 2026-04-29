using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;
using Pl.Components.Components.Switch;

namespace Pl.Components.Components;

public partial class PlSwitch : PlComponentBase
{
    #region Fields

    private FieldIdentifier _fieldIdentifier;
    private EditContext? _editContext;

    #endregion

    #region Cascading Parameters

    /// <summary>
    /// Gets or sets the cascaded EditContext from a parent EditForm.
    /// </summary>
    [CascadingParameter]
    private EditContext? CascadedEditContext { get; set; }

    #endregion

    #region Parameters - Checked

    /// <summary>
    /// Gets or sets whether the switch is checked (on).
    /// </summary>
    /// <remarks>
    /// This property supports two-way binding using the @bind-Checked directive.
    /// Changes to this property trigger the CheckedChanged event callback.
    /// </remarks>
    [Parameter]
    public bool Checked { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the checked state changes.
    /// </summary>
    /// <remarks>
    /// This event callback enables two-way binding with @bind-Checked.
    /// It is invoked whenever the user toggles the switch state.
    /// </remarks>
    [Parameter]
    public EventCallback<bool> CheckedChanged { get; set; }

    /// <summary>
    /// Gets or sets an expression that identifies the bound value.
    /// </summary>
    /// <remarks>
    /// Used for form validation integration. When provided, the switch
    /// registers with the EditContext and participates in form validation.
    /// </remarks>
    [Parameter]
    public Expression<Func<bool>>? CheckedExpression { get; set; }

    #endregion

    #region Parameters - Thumb

    /// <summary>
    /// Gets or sets additional CSS classes to apply to the switch thumb.
    /// </summary>
    [Parameter]
    public string ThumbClass { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional CSS classes applied to the thumb when checked.
    /// </summary>
    [Parameter]
    public string ThumbCheckedClass { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional CSS classes applied to the thumb when unchecked.
    /// </summary>
    [Parameter]
    public string ThumbUncheckedClass { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets custom content to render inside the switch thumb.
    /// The context parameter is a bool indicating the checked state.
    /// </summary>
    [Parameter]
    public RenderFragment<bool>? ThumbContent { get; set; }

    # endregion

    #region Parameters - Base

    /// <summary>
    /// Gets or sets whether the switch is disabled.
    /// </summary>
    /// <remarks>
    /// When disabled:
    /// - Switch cannot be clicked or focused
    /// - Opacity is reduced
    /// - Pointer events are disabled
    /// - aria-disabled attribute is set to true
    /// </remarks>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Default value is <see cref="SwitchSize.Medium"/>.
    /// </summary>
    [Parameter]
    public SwitchSize Size { get; set; } = SwitchSize.Medium;

    /// <summary>
    /// Gets or sets the ARIA label for the switch.
    /// </summary>
    /// <remarks>
    /// Provides accessible text for screen readers when the switch
    /// doesn't have associated label text.
    /// </remarks>
    [Parameter]
    public string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets the ID attribute for the switch element.
    /// </summary>
    /// <remarks>
    /// Used for associating the switch with label elements via htmlFor attribute.
    /// </remarks>
    [Parameter]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the IDs of elements that describe the switch, for accessibility.
    /// </summary>
    [Parameter]
    public string? AriaDescribedBy { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Gets the computed CSS classes for the switch track element.
    /// </summary>
    protected override string CssClass => ClassNames.Cn(
        "peer inline-flex shrink-0 cursor-pointer items-center rounded-full border-2 border-transparent",
        "transition-colors focus-visible:outline-none focus-visible:ring-2",
        "focus-visible:ring-ring focus-visible:ring-offset-2 focus-visible:ring-offset-background",
        "disabled:cursor-not-allowed disabled:opacity-50",
        Size switch
        {
            SwitchSize.Small => "h-5 w-9",
            SwitchSize.Medium => "h-6 w-11",
            SwitchSize.Large => "h-7 w-14",
            _ => "h-6 w-11"
        },
        Checked ? "bg-primary" : "bg-input",
        Class
    );

    /// <summary>
    /// Gets the computed CSS classes for the switch thumb element
    /// </summary>
    private string ThumbCssClass => ClassNames.Cn(
        "pointer-events-none block rounded-full bg-background shadow-lg ring-0 transition-transform",
        Size switch
        {
            SwitchSize.Small  => "h-4 w-4",
            SwitchSize.Medium => "h-5 w-5",
            SwitchSize.Large  => "h-6 w-6",
            _                 => "h-5 w-5"
        },
        Checked ? Size switch
        {
            SwitchSize.Small  => "translate-x-4",
            SwitchSize.Medium => "translate-x-5",
            SwitchSize.Large  => "translate-x-7",
            _                 => "translate-x-5"
        } : "translate-x-0",
        ClassNames.When( ThumbContent is not null, "flex items-center justify-center"),
        Checked ? ThumbCheckedClass : ThumbUncheckedClass,
        ThumbClass
    );

    /// <summary>
    /// Gets whether the switch is in an invalid state (for validation).
    /// </summary>
    private bool IsInvalid =>
        _editContext != null &&
        CheckedExpression != null &&
        _editContext.GetValidationMessages(_fieldIdentifier).Any();

    #endregion

    /// <summary>
    /// Handles the checked state change from the primitive and wraps form validation.
    /// </summary>
    private async Task HandleCheckedChanged(bool value)
    {
        Checked = value;
        await CheckedChanged.InvokeAsync(Checked);

        // Notify EditContext of field change for validation
        if (_editContext != null && CheckedExpression != null)
            _editContext.NotifyFieldChanged(_fieldIdentifier);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // Initialize EditContext integration if available
        if (CascadedEditContext == null || CheckedExpression == null)
            return;
        _editContext = CascadedEditContext;
        _fieldIdentifier = FieldIdentifier.Create(CheckedExpression);
    }
}