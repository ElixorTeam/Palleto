using System.Linq.Expressions;
using System.Numerics;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using static System.GC;

namespace Pl.Components;

public partial class PlInputNumber<TValue> : PlComponentBase where TValue : struct, INumber<TValue>
{
    private readonly record struct JsConfig(
        bool Debouncing,
        int DebounceMs,
        bool AllowDecimal,
        bool AllowNegative,
        string DecimalSeparator);

    private readonly EditContextFieldState _validation = new();

    private ElementReference _inputRef;
    private DotNetObjectReference<PlInputNumber<TValue>>? _dotNetRef;
    private string? _generatedId;
    private string _editingValue = string.Empty;
    private bool _disposed;
    private bool _isEditing;
    private TValue _valueAtFocus;

    /// <summary>
    /// Gets or sets the cascaded EditContext from a parent EditForm.
    /// </summary>
    [CascadingParameter]
    private EditContext? CascadedEditContext { get; set; }

    [CascadingParameter(Name = "FieldIsInvalid")]
    private bool? FieldIsInvalid { get; set; }

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
    /// Gets or sets whether negative values are allowed.
    /// </summary>
    [Parameter]
    public bool AllowNegative { get; set; } = true;

    /// <summary>
    /// Gets or sets the format string for displaying the value.
    /// </summary>
    [Parameter]
    public string? Format { get; set; }

    /// <summary>
    /// When <c>true</c>, <see cref="ValueChanged"/> is debounced during typing.
    /// When <c>false</c> (default), updates fire only on blur.
    /// </summary>
    [Parameter]
    public bool Debouncing { get; set; }

    /// <summary>
    /// Debounce delay in milliseconds when <see cref="Debouncing"/> is <c>true</c>. Default is 500 ms.
    /// </summary>
    [Parameter]
    public int DebounceInterval { get; set; } = 500;

    /// <summary>
    /// Gets or sets whether to show increment/decrement buttons.
    /// </summary>
    [Parameter]
    public bool ShowButtons { get; set; }

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
    /// Gets or sets the ARIA label.
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
    public bool? AriaInvalid { get; set; }

    /// <summary>
    /// Gets or sets the HTML name attribute for the input element.
    /// </summary>
    /// <remarks>
    /// When inside an EditForm and not explicitly set, the name is automatically
    /// derived from the ValueExpression (FieldIdentifier) to support SSR form postback.
    /// </remarks>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets an expression that identifies the bound value.
    /// </summary>
    [Parameter]
    public Expression<Func<TValue>>? ValueExpression { get; set; }

    /// <summary>
    /// Gets the effective aria-invalid value combining manual AriaInvalid and EditContext validation.
    /// </summary>
    private string? EffectiveAriaInvalid => _validation.GetEffectiveAriaInvalid(AriaInvalid, FieldIsInvalid);

    /// <summary>
    /// Gets the effective name attribute, falling back to the FieldIdentifier name when inside an EditForm.
    /// </summary>
    private string? EffectiveName => _validation.GetEffectiveName(Name);

    private string EffectiveId => Id ?? (_generatedId ??= $"numeric-{Guid.NewGuid().ToString("N")[..8]}");

    private TValue StepValue => Step ?? TValue.One;

    private bool IsAtMax => Value >= Max;
    private bool IsAtMin => Value <= Min;

    private string DisplayValue => _isEditing ? _editingValue : GetFormattedValueString();

    private static bool IsFloatingPoint =>
        typeof(TValue) == typeof(double) ||
        typeof(TValue) == typeof(float) ||
        typeof(TValue) == typeof(decimal);

    private static string InputMode => IsFloatingPoint ? "decimal" : "numeric";

    private string CssClass =>
        CssUtil.Cn(
            // Base input styles
            "flex h-8 w-full border border-input bg-transparent px-2.5 py-1 text-base",
            "transition-colors outline-none placeholder:text-muted-foreground placeholder:select-none",
            // rounded left corners only
            "rounded-l-md rounded-r-none",
            // Focus states
            "focus-visible:border-ring focus-visible:ring-3 focus-visible:ring-ring/50",
            // Error states (aria-invalid)
            "aria-invalid:border-destructive aria-invalid:focus-visible:ring-3 aria-invalid:focus-visible:ring-destructive/20",
            // Disabled state
            "disabled:pointer-events-none disabled:cursor-not-allowed disabled:bg-input/50 disabled:opacity-50",
            // Responsive text sizing
            "md:text-sm",
            // Dark mode
            "dark:bg-input/30 dark:disabled:bg-input/80",
            "dark:aria-invalid:border-destructive/50 dark:aria-invalid:focus-visible:ring-destructive/40",
            Class
        );

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

    /// <inheritdoc />
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        _validation.Update(CascadedEditContext, ValueExpression);

        if (!_isEditing)
            _valueAtFocus = Value;

        await JsModule.SyncConfigAsync(GetJsConfig());
    }

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        _dotNetRef ??= DotNetObjectReference.Create(this);
        await JsModule.SetupAsync("numeric-input", _inputRef, _dotNetRef, GetJsConfig());
    }

    /// <summary>
    /// Sets focus to the underlying input element.
    /// </summary>
    public ValueTask FocusAsync() => _inputRef.FocusAsync();

    public async ValueTask DisposeAsync()
    {
        _disposed = true;

        await JsModule.DisposeAsync();

        _dotNetRef?.Dispose();
        SuppressFinalize(this);
    }

    /// <summary>
    /// Called from JavaScript during typing. JS has already handled debounce if enabled.
    /// </summary>
    [JSInvokable("JsOnInput")]
    public async Task JsOnInput(string? value)
    {
        if (_disposed) { return; }

        string inputValue = value ?? string.Empty;
        _editingValue = inputValue;
        _isEditing = true;

        if (TryCommitValue(inputValue, out TValue committedValue))
        {
            if (!committedValue.Equals(Value))
            {
                Value = committedValue;
                await ValueChanged.InvokeAsync(committedValue);
                NotifyFieldChanged();
            }

            _valueAtFocus = committedValue;
        }

        StateHasChanged();
    }

    /// <summary>
    /// Called from JavaScript on blur. Returns formatted display text for the native input element.
    /// </summary>
    [JSInvokable("JsOnBlur")]
    public async Task<string> JsOnBlur(string? value)
    {
        if (_disposed)
            return GetFormattedValueString();

        string inputValue = value ?? string.Empty;
        _isEditing = false;

        if (TryCommitValue(inputValue, out TValue committedValue))
        {
            if (!committedValue.Equals(Value))
            {
                Value = committedValue;
                await ValueChanged.InvokeAsync(committedValue);
                NotifyFieldChanged();
            }

            _valueAtFocus = committedValue;
        }
        else
        {
            Value = _valueAtFocus;
        }

        string display = GetFormattedValueString();
        _editingValue = display;
        StateHasChanged();
        return display;
    }

    /// <summary>
    /// Called from JavaScript on focus.
    /// </summary>
    [JSInvokable("JsOnFocus")]
    public void JsOnFocus()
    {
        if (_disposed)
            return;

        _valueAtFocus = Value;
        _editingValue = GetFormattedValueString();
        _isEditing = true;
        StateHasChanged();
    }

    /// <summary>
    /// Called from JavaScript when a step key is pressed (ArrowUp/Down, PageUp/Down, Home/End).
    /// JS has already called preventDefault().
    /// </summary>
    [JSInvokable("JsOnKeyDown")]
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

    private JsConfig GetJsConfig() => new(
        Debouncing,
        DebounceInterval,
        IsFloatingPoint,
        AllowNegative,
        CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);

    private void NotifyFieldChanged() => _validation.NotifyFieldChanged();

    private string GetFormattedValueString()
    {
        if (Format != null)
            return string.Format(CultureInfo.InvariantCulture, $"{{0:{Format}}}", Value);

        if (DecimalPlaces.HasValue && IsFloatingPoint)
            return string.Format(CultureInfo.InvariantCulture, $"{{0:F{DecimalPlaces.Value}}}", Value);

        return Value.ToString() ?? string.Empty;
    }

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

    private async Task SetValue(TValue value)
    {
        TValue clampedValue = ClampValue(value);

        if (!clampedValue.Equals(Value))
        {
            Value = clampedValue;
            _editingValue = clampedValue.ToString() ?? string.Empty;
            await ValueChanged.InvokeAsync(clampedValue);
            NotifyFieldChanged();
        }
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

    private bool TryCommitValue(string? input, out TValue result)
    {
        result = _valueAtFocus;

        if (!TryParseValue(input, out TValue parsed))
            return false;

        if (!IsWithinRange(parsed))
            return false;

        result = parsed;
        return true;
    }

    private bool IsWithinRange(TValue value)
    {
        if (!AllowNegative && value < TValue.Zero)
            return false;

        if (value < Min)
            return false;

        return !(value > Max);
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
}
