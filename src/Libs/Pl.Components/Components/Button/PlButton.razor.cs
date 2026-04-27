using BlazorBlueprint.Primitives.Utilities;

namespace Pl.Components.Components;

public partial class PlButton : LmsComponentBase
{
    #region Fields

    /// <summary>
    /// Reference to the button element for positioning support when used with AsChild.
    /// </summary>
    private ElementReference _buttonRef;

    /// <summary>
    /// Tracks the previous TriggerContext to detect when it changes.
    /// </summary>
    private TriggerContext? _previousTriggerContext;

    #endregion

    #region Cascading Parameters

    /// <summary>
    /// Gets the trigger context from a parent trigger component when using AsChild pattern.
    /// </summary>
    /// <remarks>
    /// When a Button is used as a child of a trigger component with AsChild=true,
    /// it automatically receives this context to handle trigger behavior (click to toggle,
    /// aria attributes, etc.).
    /// </remarks>
    [CascadingParameter(Name = "TriggerContext")]
    public TriggerContext? TriggerContext { get; set; }

    #endregion

    #region Parameters - Variants

    /// <summary>
    /// Default value is <see cref="ButtonVariant.Default"/>.
    /// </summary>
    [Parameter]
    public ButtonVariant Variant { get; set; } = ButtonVariant.Default;

    /// <summary>
    /// Default value is <see cref="ButtonSize.Default"/>.
    /// </summary>
    [Parameter]
    public ButtonSize Size { get; set; } = ButtonSize.Default;

    /// <summary>
    /// Default value is <see cref="ButtonType.Button"/>
    /// </summary>
    [Parameter]
    public ButtonType Type { get; set; } = ButtonType.Button;

    #endregion

    #region Parameters - Icon

    /// <summary>
    /// Gets or sets the icon to display in the button.
    /// </summary>
    /// <remarks>
    /// Can be any RenderFragment (SVG, icon font, image).
    /// Position is controlled by <see cref="IconPosition"/>.
    /// </remarks>
    [Parameter]
    public RenderFragment? Icon { get; set; }

    /// <summary>
    /// Default value is <see cref="IconPosition.Start"/>
    /// </summary>
    [Parameter]
    public IconPosition IconPosition { get; set; } = IconPosition.Start;

    #endregion

    #region Parameters - Loading

    /// <summary>
    /// Gets or sets whether the button is in a loading state.
    /// </summary>
    /// <remarks>
    /// When true, the button is disabled, a spinner is shown (or <see cref="LoadingTemplate"/> if provided),
    /// and <see cref="LoadingText"/> replaces the button text if specified.
    /// </remarks>
    [Parameter]
    public bool Loading { get; set; }

    /// <summary>
    /// Gets or sets the text to display while the button is loading.
    /// </summary>
    /// <remarks>
    /// When set and <see cref="Loading"/> is true, this text replaces <see cref="ChildContent"/>.
    /// </remarks>
    [Parameter]
    public string LoadingText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a custom loading indicator template.
    /// </summary>
    /// <remarks>
    /// When set and <see cref="Loading"/> is true, this template is rendered instead of the default spinner.
    /// </remarks>
    [Parameter]
    public RenderFragment? LoadingTemplate { get; set; }

    #endregion

    #region Parameters - Link

    /// <summary>
    /// Gets or sets the URL to navigate to.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Gets or sets the anchor target attribute (e.g., "_blank"). Only applies when <see cref="Href"/> is set.
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    #endregion

    #region Parameters - Base

    /// <summary>
    /// Gets or sets whether the button is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the content to be rendered inside the button.
    /// Can contain text, icons, or any other Blazor markup.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    #endregion

    #region Parameters - Events & Accessibility

    /// <summary>
    /// Gets or sets the callback invoked when the button is clicked.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Required for icon-only buttons to provide accessible text for screen readers.
    /// Optional for buttons with text content.
    /// </summary>
    [Parameter]
    public string? AriaLabel { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Gets whether the component should render as an anchor element.
    /// </summary>
    private bool HasHref => !string.IsNullOrEmpty(Href);

    /// <summary>
    /// Gets whether the button is effectively disabled (explicitly disabled or loading).
    /// </summary>
    private bool IsDisabled => Disabled || Loading;

    /// <summary>
    /// Returns "noopener noreferrer" when Target is "_blank" for security.
    /// </summary>
    private string? Rel => Target == "_blank" ? "noopener noreferrer" : null;

    /// <summary>
    /// Gets the HTML button type attribute value.
    /// </summary>
    private string HtmlType => Type switch
    {
        ButtonType.Submit => "submit",
        ButtonType.Reset => "reset",
        _ => "button"
    };

    /// <summary>
    /// Gets the computed CSS classes for the button element.
    /// </summary>
    private string CssClass => ClassNames.Cn(
        "inline-flex items-center justify-center gap-2 rounded-md text-sm font-medium",
        "transition-colors focus-visible:outline-none focus-visible:ring-2",
        "focus-visible:ring-ring focus-visible:ring-offset-2",
        "cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed",
        Variant switch
        {
            ButtonVariant.Default => "bg-primary text-primary-foreground hover:bg-primary/90",
            ButtonVariant.Destructive => "bg-destructive text-destructive-foreground hover:bg-destructive/90",
            ButtonVariant.Outline => "border border-input bg-background hover:bg-accent hover:text-accent-foreground",
            ButtonVariant.Secondary => "bg-secondary text-secondary-foreground hover:bg-secondary/80",
            ButtonVariant.Ghost => "hover:bg-accent hover:text-accent-foreground",
            ButtonVariant.Link => "text-primary underline-offset-4 hover:underline",
            _ => "bg-primary text-primary-foreground hover:bg-primary/90"
        },
        Size switch
        {
            ButtonSize.Small => "h-9 rounded-md px-3 text-xs",
            ButtonSize.Default => "h-10 px-4 py-2",
            ButtonSize.Large => "h-11 rounded-md px-8",
            ButtonSize.Icon => "h-10 w-10",
            ButtonSize.IconSmall => "h-9 w-9",
            ButtonSize.IconLarge => "h-11 w-11",
            _ => "h-10 px-4 py-2"
        },
        ClassNames.When(HasHref && IsDisabled, "pointer-events-none cursor-not-allowed opacity-50"),
        Class
    );

    #endregion

    #region Lifecycle

    /// <summary>
    /// Registers the button element reference with the trigger context for positioning.
    /// Re-registers when TriggerContext changes (e.g. parent re-renders inside Dialog/Sheet).
    /// </summary>
    protected override void OnAfterRender(bool firstRender)
    {
        if (TriggerContext?.SetTriggerElement == null ||
            (!firstRender && TriggerContext == _previousTriggerContext))
        {
            return;
        }

        TriggerContext.SetTriggerElement.Invoke(_buttonRef);
        _previousTriggerContext = TriggerContext;
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// Handles the button click event.
    /// </summary>
    /// <param name="args">The mouse event arguments.</param>
    /// <remarks>
    /// This method is invoked when the button is clicked and not disabled.
    /// If a TriggerContext is present (from AsChild pattern), it invokes Toggle or Close.
    /// It also triggers the OnClick callback if one is registered.
    /// </remarks>
    private async Task HandleClick(MouseEventArgs args)
    {
        if (IsDisabled)
            return;

        if (TriggerContext != null)
        {
            if (TriggerContext.Toggle == null && TriggerContext.Close != null)
                TriggerContext.Close?.Invoke();
            else
                TriggerContext.Toggle?.Invoke();
        }

        if (OnClick.HasDelegate)
            await OnClick.InvokeAsync(args);
    }

    /// <summary>
    /// Handles keyboard events for trigger context (dropdown menu arrow keys, etc.).
    /// </summary>
    private async Task HandleKeyDown(KeyboardEventArgs args)
    {
        if (IsDisabled)
            return;

        if (TriggerContext?.OnKeyDown != null)
            await TriggerContext.OnKeyDown.Invoke(args);
    }

    /// <summary>
    /// Handles mouse enter events for hover-triggered components (Tooltip, HoverCard).
    /// </summary>
    private void HandleMouseEnter() => TriggerContext?.OnMouseEnter?.Invoke();

    /// <summary>
    /// Handles mouse leave events for hover-triggered components.
    /// </summary>
    private void HandleMouseLeave() => TriggerContext?.OnMouseLeave?.Invoke();

    /// <summary>
    /// Handles focus events for focus-triggered components.
    /// </summary>
    private void HandleFocus() => TriggerContext?.OnFocus?.Invoke();

    /// <summary>
    /// Handles blur events for focus-triggered components.
    /// </summary>
    private void HandleBlur() => TriggerContext?.OnBlur?.Invoke();

    #endregion
}