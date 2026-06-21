using System.Linq.Expressions;
using System.Numerics;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using static System.GC;

namespace Pl.Components;

public partial class PlInputNumber<TValue> : PlComponentBase
    where TValue : struct, INumber<TValue>
{
    #region Fields

    /// <summary>
    /// Unique instance identifier passed to the numeric-input JS module for lifecycle tracking.
    /// </summary>
    private readonly string _instanceId = Guid.NewGuid().ToString("N");

    /// <summary>
    /// Handles EditForm field change notifications and validation state.
    /// </summary>
    private readonly EditContextFieldState _validation = new();

    /// <summary>
    /// Reference to the native input element for JS interop initialization.
    /// </summary>
    private ElementReference _inputRef;

    private IJSObjectReference? _jsModule;
    private DotNetObjectReference<PlInputNumber<TValue>>? _dotNetRef;

    private bool _disposed;
    private bool _isEditing;
    private bool _jsInitialized;

    private string? _generatedId;
    private string _editingValue = string.Empty;
    private TValue _valueAtFocus;

    #endregion

    #region Cascading Parameters

    /// <summary>
    /// Gets the cascaded <see cref="EditContext"/> from a parent <see cref="EditForm"/>.
    /// </summary>
    [CascadingParameter]
    private EditContext? CascadedEditContext { get; set; }

    [CascadingParameter(Name = "FieldIsInvalid")]
    private bool? FieldIsInvalid { get; set; }

    #endregion

    #region Parameters - Value & Binding

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    [Parameter]
    public TValue Value { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets an expression that identifies the bound value for EditForm integration.
    /// Automatically provided by <c>@bind-Value</c>.
    /// </summary>
    [Parameter]
    public Expression<Func<TValue>>? ValueExpression { get; set; }

    #endregion

    #region Parameters - Constraints

    /// <summary>
    /// Gets or sets the minimum allowed value.
    /// </summary>
    [Parameter]
    public TValue? Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum allowed value.
    /// </summary>
    [Parameter]
    public TValue? Max { get; set; }

    /// <summary>
    /// Gets or sets the step increment for arrow key/button changes.
    /// </summary>
    [Parameter]
    public TValue? Step { get; set; }

    /// <summary>
    /// Gets or sets the number of decimal places to display (for decimal/double/float types).
    /// </summary>
    [Parameter]
    public int? DecimalPlaces { get; set; }

    /// <summary>
    /// Default value is <c>true</c>.
    /// </summary>
    [Parameter]
    public bool AllowNegative { get; set; } = true;

    #endregion

    #region Parameters - Display

    /// <summary>
    /// Gets or sets the format string for displaying the value.
    /// </summary>
    [Parameter]
    public string? Format { get; set; }

    /// <summary>
    /// Default value is <see cref="JsSyncTiming.OnBlur"/>.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><see cref="JsSyncTiming.Immediate"/> — every keystroke (batched via requestAnimationFrame).</item>
    /// <item><see cref="JsSyncTiming.OnBlur"/> — only on blur (default).</item>
    /// <item><see cref="JsSyncTiming.Debounced"/> — after typing pauses for <see cref="DebounceInterval"/> ms.</item>
    /// </list>
    /// </remarks>
    [Parameter]
    public JsSyncTiming JsSyncTiming { get; set; } = JsSyncTiming.OnBlur;

    /// <summary>
    /// Default value is <c>300</c>. Used when <see cref="JsSyncTiming"/> is <see cref="JsSyncTiming.Debounced"/>.
    /// </summary>
    [Parameter]
    public int DebounceInterval { get; set; } = 300;

    #endregion

    #region Parameters - Base

    /// <summary>
    /// Gets or sets the placeholder text.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets whether the input is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the input is required.
    /// </summary>
    [Parameter]
    public bool Required { get; set; }

    /// <summary>
    /// Gets or sets the HTML id attribute.
    /// </summary>
    [Parameter]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the HTML name attribute. Auto-derived from <see cref="ValueExpression"/>
    /// when inside an EditForm if not explicitly set.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    #endregion

    #region Parameters - Events & Accessibility

    /// <summary>
    /// Gets or sets the ARIA label for the input.
    /// </summary>
    [Parameter]
    public string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets the ID of the element that describes the input.
    /// </summary>
    [Parameter]
    public string? AriaDescribedBy { get; set; }

    /// <summary>
    /// Gets or sets whether the input value is invalid.
    /// </summary>
    [Parameter]
    public bool AriaInvalid { get; set; }

    #endregion

    #region Computed Properties

    private static bool IsFloatingPoint =>
        typeof(TValue) == typeof(double) || typeof(TValue) == typeof(float) || typeof(TValue) == typeof(decimal);

    private static string InputMode => IsFloatingPoint ? "decimal" : "numeric";

    private TValue StepValue => Step ?? TValue.One;

    private bool IsAtMax => Value >= Max;

    private bool IsAtMin => Value <= Min;

    /// <summary>
    /// Gets the effective aria-invalid value combining manual AriaInvalid and EditContext validation.
    /// </summary>
    private string? EffectiveAriaInvalid => _validation.GetEffectiveAriaInvalid(AriaInvalid, FieldIsInvalid);

    /// <summary>
    /// Gets the effective name attribute, falling back to the FieldIdentifier name when inside an EditForm.
    /// </summary>
    private string? EffectiveName => _validation.GetEffectiveName(Name);

    private string EffectiveId => Id ?? (_generatedId ??= $"numeric-{Guid.NewGuid().ToString("N")[..8]}");

    private string DisplayValue => _isEditing ? _editingValue : GetFormattedValueString();

    /// <summary>
    /// Gets the computed CSS classes for the spinbutton group wrapper.
    /// </summary>
    private static string ContainerClass => "flex items-center gap-1";

    /// <summary>
    /// Gets the computed CSS classes for the numeric input element.
    /// </summary>
    private string CssClass =>
        CssUtil.Cn(
            // Base input styles
            "flex h-8 w-full border border-input bg-transparent px-2.5 py-1 text-base",
            "transition-colors outline-none placeholder:text-muted-foreground",
            // rounded left corners only
            "rounded-l-md rounded-r-none",
            // Focus states
            "focus-visible:border-ring focus-visible:ring-3 focus-visible:ring-ring/50",
            // Error states (aria-invalid)
            "aria-invalid:border-destructive aria-invalid:ring-3 aria-invalid:ring-destructive/20",
            // Disabled state
            "disabled:pointer-events-none disabled:cursor-not-allowed disabled:bg-input/50 disabled:opacity-50",
            // Responsive text sizing
            "md:text-sm",
            // Dark mode
            "dark:bg-input/30 dark:disabled:bg-input/80",
            "dark:aria-invalid:border-destructive/50 dark:aria-invalid:ring-destructive/40",
            Class
        );

    /// <summary>
    /// Gets the computed CSS classes for the stepper buttons.
    /// </summary>
    private static string ButtonClass =>
        CssUtil.Cn(
            // Base button styles
            "flex items-center justify-center w-8 h-4 border border-input bg-transparent",
            "hover:bg-accent hover:text-accent-foreground",
            "focus-visible:outline-none",
            // Disabled state
            "disabled:cursor-not-allowed disabled:opacity-50",
            // Spinbutton layout — merge up/down buttons, round outer right corners
            "first:border-b-0 rounded-l-none first:rounded-tr-md last:rounded-br-md",
            "transition-colors"
        );

    #endregion

    #region Lifecycle

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _validation.Update(CascadedEditContext, ValueExpression);
    }

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        try
        {
            _jsModule = await JsRuntime.ImportJsModuleAsync("input-number");
            _dotNetRef = DotNetObjectReference.Create(this);
            await _jsModule.InvokeVoidAsync("initialize", _inputRef, _dotNetRef, _instanceId, GetJsConfig());
            _jsInitialized = true;
        }
        catch (Exception ex) when (ex is JSDisconnectedException or TaskCanceledException or ObjectDisposedException)
        {
            // Expected during circuit disconnect
        }
        catch (InvalidOperationException)
        {
            // JS interop not available during prerendering
        }
    }

    public async ValueTask DisposeAsync()
    {
        _disposed = true;

        if (_jsModule is not null && _jsInitialized)
        {
            try
            {
                await _jsModule.InvokeVoidAsync("dispose", _instanceId);
                await _jsModule.DisposeAsync();
            }
            catch (Exception ex)
                when (ex is JSDisconnectedException or TaskCanceledException or ObjectDisposedException)
            {
                // Expected during circuit disconnect
            }
            catch (InvalidOperationException)
            {
                // JS interop not available
            }
        }

        _dotNetRef?.Dispose();
        SuppressFinalize(this);
    }

    #endregion

    #region JS Interop

    /// <summary>
    /// Builds the JS configuration object from current parameters.
    /// </summary>
    private object GetJsConfig() =>
        new
        {
            mode = JsSyncTiming.ToJsValue(),
            debounceMs = DebounceInterval,
            stepKeys = new[] { "ArrowUp", "ArrowDown", "PageUp", "PageDown", "Home", "End" },
            allowDecimal = IsFloatingPoint,
            allowNegative = AllowNegative,
            decimalSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator,
        };

    /// <summary>
    /// Called from JavaScript during typing.
    /// </summary>
    /// <param>
    /// When <c>false</c>, only syncs the edit buffer for display.
    /// When <c>true</c>, also tries to commit <see cref="Value"/> (Immediate/Debounced).
    /// </param>
    [JSInvokable]
    public async Task JsOnInput(string? value)
    {
        if (_disposed)
            return;

        string inputValue = value ?? string.Empty;
        _editingValue = inputValue;
        _isEditing = true;

        if (TryCommitValue(inputValue, out TValue committedValue) && !committedValue.Equals(Value))
        {
            Value = committedValue;
            await ValueChanged.InvokeAsync(committedValue);
            NotifyFieldChanged();
        }

        StateHasChanged();
    }

    /// <summary>
    /// Called from JavaScript on blur. Commits the current value.
    /// Returns the formatted display text because Blazor may skip updating the native
    /// input when <see cref="DisplayValue"/> did not change between renders (e.g. overflow revert).
    /// </summary>
    [JSInvokable]
    public async Task<string> JsOnBlur(string? value)
    {
        if (_disposed)
            return GetFormattedValueString();

        string inputValue = value ?? string.Empty;
        _isEditing = false;

        if (JsSyncTiming == JsSyncTiming.OnBlur)
        {
            if (TryCommitValue(inputValue, out TValue committedValue))
            {
                if (!committedValue.Equals(Value))
                {
                    Value = committedValue;
                    await ValueChanged.InvokeAsync(committedValue);
                }
            }
            else
            {
                Value = _valueAtFocus;
            }

            NotifyFieldChanged();
        }
        else if (TryCommitValue(inputValue, out TValue committedValue))
        {
            if (!committedValue.Equals(Value))
            {
                Value = committedValue;
                await ValueChanged.InvokeAsync(committedValue);
                NotifyFieldChanged();
            }
        }

        string displayValue = GetFormattedValueString();
        _editingValue = displayValue;
        StateHasChanged();
        return displayValue;
    }

    /// <summary>
    /// Called from JavaScript on focus.
    /// </summary>
    [JSInvokable]
    public void JsOnFocus()
    {
        if (_disposed)
            return;

        _valueAtFocus = Value;
        _editingValue = Value.ToString() ?? string.Empty;
        _isEditing = true;
        StateHasChanged();
    }

    /// <summary>
    /// Called from JavaScript when a step key is pressed (ArrowUp/Down, PageUp/Down, Home/End).
    /// JS has already called preventDefault().
    /// </summary>
    [JSInvokable]
    public async Task JsOnKeyDown(string key)
    {
        if (_disposed || Disabled)
            return;

        switch (key)
        {
            case "ArrowUp":
                await Increment();
                break;
            case "ArrowDown":
                await Decrement();
                break;
            case "PageUp":
                await IncrementBy(TValue.CreateChecked(10) * StepValue);
                break;
            case "PageDown":
                await DecrementBy(TValue.CreateChecked(10) * StepValue);
                break;
            case "Home":
                if (Min.HasValue)
                    await SetValue(Min.Value);
                break;
            case "End":
                if (Max.HasValue)
                    await SetValue(Max.Value);
                break;
        }

        StateHasChanged();
    }

    #endregion

    #region Event Handlers

    private async Task Increment()
    {
        if (Disabled || IsAtMax)
            return;

        await SetValue(Value + StepValue);
    }

    private async Task Decrement()
    {
        if (Disabled || IsAtMin)
            return;

        await SetValue(Value - StepValue);
    }

    private async Task IncrementBy(TValue amount)
    {
        if (Disabled)
            return;

        await SetValue(Value + amount);
    }

    private async Task DecrementBy(TValue amount)
    {
        if (Disabled)
            return;

        await SetValue(Value - amount);
    }

    #endregion

    #region Private Helpers

    private void NotifyFieldChanged() => _validation.NotifyFieldChanged();

    private async Task SetValue(TValue value)
    {
        TValue clampedValue = ClampValue(value);

        if (clampedValue.Equals(Value))
            return;

        Value = clampedValue;
        _editingValue = clampedValue.ToString() ?? string.Empty;
        await ValueChanged.InvokeAsync(clampedValue);
        NotifyFieldChanged();
    }

    private string GetFormattedValueString()
    {
        if (Format is not null)
            return string.Format(CultureInfo.InvariantCulture, $"{{0:{Format}}}", Value);

        if (DecimalPlaces.HasValue && IsFloatingPoint)
            return string.Format(CultureInfo.InvariantCulture, $"{{0:F{DecimalPlaces.Value}}}", Value);

        return Value.ToString() ?? string.Empty;
    }

    private bool IsWithinRange(TValue value)
    {
        if (!AllowNegative && value < TValue.Zero)
            return false;

        if (value < Min)
            return false;

        return !Max.HasValue || value <= Max.Value;
    }

    private bool TryCommitValue(string? input, out TValue result)
    {
        result = default;

        if (!TryParseValue(input, out TValue parsedValue))
            return false;

        if (!IsWithinRange(parsedValue))
            return false;

        result = parsedValue;
        return true;
    }

    private TValue ClampValue(TValue value)
    {
        if (!AllowNegative && value < TValue.Zero)
            value = TValue.Zero;

        if (value < Min)
            value = Min.Value;

        if (value > Max)
            value = Max.Value;

        return value;
    }

    private bool TryParseValue(string? input, out TValue result)
    {
        result = default;

        if (string.IsNullOrWhiteSpace(input))
        {
            result = TValue.Zero;
            return true;
        }

        input = input.Replace(",", "").Trim();

        if (!AllowNegative && input.StartsWith('-'))
            return false;

        return TValue.TryParse(input, CultureInfo.InvariantCulture, out result);
    }

    #endregion
}
