using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using static System.GC;

namespace Pl.Components.Components;

public partial class PlInput<TValue> : ComponentBase
{
    #region Fields

    /// <summary>
    /// Unique instance identifier passed to the text-input JS module for lifecycle tracking.
    /// </summary>
    private readonly string _instanceId = Guid.NewGuid().ToString("N");

    /// <summary>
    /// Handles EditForm field change notifications and validation state.
    /// </summary>
    private readonly InputValidationBehavior _validation = new();

    /// <summary>
    /// Reference to the native input element for JS interop initialization.
    /// </summary>
    private ElementReference _inputRef;

    private string? _generatedId;
    private string _editingValue = string.Empty;

    private bool _disposed;
    private bool _isEditing;
    private bool _hasParseError;
    private bool _jsInitialized;

    private IJSObjectReference? _jsModule;
    private EditContext? _subscribedEditContext;
    private InputFieldErrorKind? _currentErrorKind;
    private InputConverter<TValue>? _cachedDefaultConverter;
    private DotNetObjectReference<PlInput<TValue>>? _dotNetRef;

    #endregion

    #region Cascading Parameters

    [CascadingParameter]
    private EditContext? CascadedEditContext { get; set; }

    #endregion

    #region Parameters - Value & Binding

    /// <summary>
    /// Gets or sets the current typed value.
    /// </summary>
    /// <remarks>
    /// Supports two-way binding via @bind-Value syntax.
    /// The value is converted to/from string using the converter resolution chain.
    /// </remarks>
    [Parameter]
    public TValue? Value { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the typed value changes.
    /// </summary>
    [Parameter]
    public EventCallback<TValue?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the expression identifying the bound value for EditForm integration.
    /// Automatically provided by <c>@bind-Value</c>.
    /// </summary>
    [Parameter]
    public Expression<Func<TValue?>>? ValueExpression { get; set; }

    #endregion

    #region Parameters - Conversion & Format

    /// <summary>
    /// Gets or sets an optional custom converter for this component instance.
    /// </summary>
    /// <remarks>
    /// When provided, the converter's <see cref="InputConverter{TValue}.GetFunc"/> and
    /// <see cref="InputConverter{TValue}.SetFunc"/> take highest priority in the resolution chain.
    /// </remarks>
    [Parameter]
    public InputConverter<TValue>? Converter { get; set; }

    /// <summary>
    /// Gets or sets the display format string.
    /// </summary>
    /// <remarks>
    /// When set, uses <see cref="IFormattable.ToString(string, IFormatProvider)"/> for display
    /// formatting (e.g., "yyyy-MM-dd" for dates, "N2" for numbers). Format only affects display;
    /// parsing always uses the converter's <c>Get</c> function.
    /// </remarks>
    [Parameter]
    public string? Format { get; set; }

    #endregion

    #region Parameters - Validation

    /// <summary>
    /// Gets or sets a post-parse validation function.
    /// </summary>
    /// <remarks>
    /// Called after successful parsing to validate the typed value.
    /// Return <c>true</c> if the value is valid, <c>false</c> to reject it.
    /// </remarks>
    [Parameter]
    public Func<TValue, bool>? Validation { get; set; }

    /// <summary>
    /// Gets or sets a regex pattern for pre-parse validation on the raw string.
    /// </summary>
    /// <remarks>
    /// When set, the raw input string is validated against this pattern before parsing.
    /// Uses <see cref="Regex.IsMatch(string, string)"/>.
    /// </remarks>
    [Parameter]
    public string? ValidationPattern { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when a parse or validation error occurs on blur.
    /// </summary>
    /// <remarks>
    /// Fires when the user leaves the input with a value that cannot be converted to
    /// <typeparamref name="TValue"/> or fails validation. During typing, errors are silently ignored.
    /// The <see cref="InputParseException.ErrorKind"/> property indicates the specific failure type.
    /// </remarks>
    [Parameter]
    public EventCallback<InputParseException> OnParseError { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the error state clears.
    /// </summary>
    /// <remarks>
    /// Fires when the input transitions from an error state back to a valid state,
    /// either because the user entered a valid value during typing or cleared the input.
    /// Use this to clear error messages in the parent component.
    /// </remarks>
    [Parameter]
    public EventCallback OnErrorCleared { get; set; }

    #endregion

    #region Parameters - Input

    /// <summary>
    /// Default value is <see cref="InputType.Text"/>.
    /// </summary>
    [Parameter]
    public InputType Type { get; set; } = InputType.Text;

    /// <summary>
    /// Gets or sets the placeholder text displayed when the input is empty.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets the HTML id attribute for the input element.
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

    #region Parameters - Base

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
    /// Gets or sets additional CSS classes to apply to the input.
    /// </summary>
    [Parameter]
    public string Class { get; set; } = string.Empty;

    #endregion

    #region Parameters - Update Timing

    /// <summary>
    /// Default value is <see cref="Components.UpdateTiming.OnBlur"/>.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><see cref="UpdateTiming.Immediate"/> — every keystroke (batched via requestAnimationFrame).</item>
    /// <item><see cref="Components.UpdateTiming.OnBlur"/> — only on blur (default).</item>
    /// <item><see cref="UpdateTiming.Debounced"/> — after typing pauses for <see cref="DebounceInterval"/> ms.</item>
    /// </list>
    /// </remarks>
    [Parameter]
    public UpdateTiming UpdateTiming { get; set; } = UpdateTiming.OnBlur;

    /// <summary>
    /// Default value is <c>500</c>.
    /// </summary>
    [Parameter]
    public int DebounceInterval { get; set; } = 500;

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
    /// <remarks>
    /// When true, aria-invalid="true" is set. This is combined with <see cref="HasParseError"/>
    /// so that the destructive border CSS is applied when either is true.
    /// </remarks>
    [Parameter]
    public bool? AriaInvalid { get; set; }

    #endregion

    #region Public API

    /// <summary>
    /// Gets whether the input currently has a parse or validation error.
    /// </summary>
    /// <remarks>
    /// Set to <c>true</c> on blur when parsing or validation fails. Auto-clears when a valid
    /// value is entered. Can be used by consumers to conditionally display error messages.
    /// </remarks>
    public bool HasParseError => _hasParseError;

    /// <summary>
    /// Gets the kind of error currently active, or <c>null</c> if no error.
    /// </summary>
    /// <remarks>
    /// Provides finer-grained error information than <see cref="HasParseError"/>.
    /// Returns <see cref="InputFieldErrorKind.Parse"/> for conversion failures,
    /// <see cref="InputFieldErrorKind.PatternValidation"/> for regex failures,
    /// and <see cref="InputFieldErrorKind.ValueValidation"/> for post-parse validation failures.
    /// </remarks>
    public InputFieldErrorKind? CurrentErrorKind => _currentErrorKind;

    #endregion

    #region Computed Properties

    private InputConverter<TValue> ResolvedConverter => Converter ?? (_cachedDefaultConverter ??= new());

    private string? EffectiveName => _validation.GetEffectiveName(Name);

    private string EffectiveId => Id ?? (_generatedId ??= $"inputfield-{Guid.NewGuid().ToString("N")[..8]}");

    private bool? ComputedAriaInvalid => (AriaInvalid == true || _hasParseError || _validation.IsInvalid) ? true : AriaInvalid;

    private string DisplayValue
    {
        get
        {
            if (_isEditing)
                return _editingValue;

            // Preserve the invalid text so the user can see what they typed wrong
            if (_hasParseError)
                return _editingValue;

            if (Value is null)
                return string.Empty;

            if (Format is not null)
                return ResolvedConverter.SetWithFormat(Value, Format) ?? string.Empty;

            return ResolvedConverter.Set(Value) ?? string.Empty;
        }
    }

    private string CssClass => CssUtil.Cn(
        "flex h-10 w-full rounded-md border border-input bg-background px-3 py-2 text-base",
        "file:border-0 file:bg-transparent file:text-sm file:font-medium file:text-foreground",
        "placeholder:text-muted-foreground",
        "focus-visible:outline-none",
        "disabled:cursor-not-allowed disabled:opacity-50",
        "aria-[invalid=true]:border-destructive",
        "transition-colors",
        "md:text-sm",
        Class
    );

    #endregion

    #region Lifecycle

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (CascadedEditContext != _subscribedEditContext)
        {
            if (_subscribedEditContext is not null)
                _subscribedEditContext.OnValidationStateChanged -= OnValidationStateChanged;

            _subscribedEditContext = CascadedEditContext;

            if (_subscribedEditContext is not null)
                _subscribedEditContext.OnValidationStateChanged += OnValidationStateChanged;
        }

        _validation.Update(CascadedEditContext, ValueExpression);
    }

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                _jsModule = await JsRuntime.ImportJsModuleAsync("text-input");
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
    }

    public async ValueTask DisposeAsync()
    {
        _disposed = true;

        if (_subscribedEditContext is not null)
            _subscribedEditContext.OnValidationStateChanged -= OnValidationStateChanged;

        if (_jsModule != null && _jsInitialized)
        {
            try
            {
                await _jsModule.InvokeVoidAsync("dispose", _instanceId);
                await _jsModule.DisposeAsync();
            }
            catch (Exception ex) when (ex is JSDisconnectedException or TaskCanceledException or ObjectDisposedException)
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
    private object GetJsConfig() => new
    {
        mode = UpdateTiming switch
        {
            UpdateTiming.Immediate => "immediate",
            UpdateTiming.OnBlur => "onchange",
            UpdateTiming.Debounced => "debounced",
            _ => "onchange"
        },
        debounceMs = DebounceInterval,
        notifyOnBlur = true
    };

    /// <summary>
    /// Called from JavaScript during typing (Immediate/Debounced modes only).
    /// JS has already handled batching (rAF) or debouncing (setTimeout).
    /// </summary>
    [JSInvokable]
    public async Task JsOnInput(string? value)
    {
        if (_disposed)
            return;

        string inputValue = value ?? string.Empty;
        _editingValue = inputValue;
        _isEditing = true;

        // Try to parse in real-time; silently ignore errors during typing
        try
        {
            if (string.IsNullOrEmpty(inputValue))
            {
                TValue? defaultValue = default;
                if (!EqualityComparer<TValue?>.Default.Equals(Value, defaultValue))
                {
                    Value = defaultValue;
                    await ValueChanged.InvokeAsync(defaultValue);
                }

                await ClearErrorState();
                NotifyFieldChanged();
                StateHasChanged();
                return;
            }

            if (ValidationPattern is not null && !Regex.IsMatch(inputValue, ValidationPattern))
            {
                StateHasChanged();
                return;
            }

            TValue parsed = ResolvedConverter.Get(inputValue);

            if (Validation is not null && !Validation(parsed))
            {
                StateHasChanged();
                return;
            }

            if (!EqualityComparer<TValue?>.Default.Equals(Value, parsed))
            {
                Value = parsed;
                await ValueChanged.InvokeAsync(parsed);
            }

            await ClearErrorState();
        }
        catch
        {
            // Silently ignore parse errors during typing
        }

        NotifyFieldChanged();
        StateHasChanged();
    }

    /// <summary>
    /// Called from JavaScript on blur (all modes) and on Enter key via change event.
    /// Handles parsing with error reporting and switches from editing to display mode.
    /// </summary>
    [JSInvokable]
    public async Task JsOnChange(string? value)
    {
        if (_disposed)
            return;

        string inputValue = value ?? string.Empty;
        _editingValue = inputValue;
        _isEditing = false;

        if (string.IsNullOrEmpty(inputValue))
        {
            TValue? defaultValue = default;
            if (!EqualityComparer<TValue?>.Default.Equals(Value, defaultValue))
            {
                Value = defaultValue;
                await ValueChanged.InvokeAsync(defaultValue);
            }

            await ClearErrorState();
            NotifyFieldChanged();
            StateHasChanged();
            return;
        }

        try
        {
            if (ValidationPattern is not null && !Regex.IsMatch(inputValue, ValidationPattern))
            {
                throw new InputFieldValidationException(InputFieldErrorKind.PatternValidation,
                    $"Input '{inputValue}' does not match validation pattern.");
            }

            TValue parsed = ResolvedConverter.Get(inputValue);

            if (Validation is not null && !Validation(parsed))
            {
                throw new InputFieldValidationException(InputFieldErrorKind.ValueValidation,
                    "Value failed validation.");
            }

            if (!EqualityComparer<TValue?>.Default.Equals(Value, parsed))
            {
                Value = parsed;
                await ValueChanged.InvokeAsync(parsed);
            }

            await ClearErrorState();
        }
        catch (InputFieldValidationException ex)
        {
            await SetErrorState(ex.ErrorKind, ex);
        }
        catch (Exception ex)
        {
            await SetErrorState(InputFieldErrorKind.Parse, ex);
        }

        NotifyFieldChanged();
        StateHasChanged();
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// Handles the focus event. Sets editing mode and loads the raw (unformatted) value.
    /// Kept as a Blazor @onfocus handler since focus is a single event per interaction.
    /// </summary>
    private void HandleFocus(FocusEventArgs args)
    {
        if (_hasParseError)
        {
            // Preserve the invalid text so the user can correct it
            _isEditing = true;
            return;
        }

        if (Value is null)
        {
            _editingValue = string.Empty;
        }
        else
        {
            // Show unformatted value while editing
            _editingValue = ResolvedConverter.Set(Value) ?? string.Empty;
        }

        _isEditing = true;
    }

    #endregion

    #region Private Helpers

    private void NotifyFieldChanged() => _validation.NotifyFieldChanged();

    private void OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e) =>
        StateHasChanged();

    /// <summary>
    /// Clears the error state and fires <see cref="OnErrorCleared"/> if transitioning from error to valid.
    /// </summary>
    private async Task ClearErrorState()
    {
        if (!_hasParseError)
            return;

        _hasParseError = false;
        _currentErrorKind = null;

        if (OnErrorCleared.HasDelegate)
            await OnErrorCleared.InvokeAsync();
    }

    /// <summary>
    /// Sets the error state and fires <see cref="OnParseError"/>.
    /// </summary>
    private async Task SetErrorState(InputFieldErrorKind errorKind, Exception ex)
    {
        _hasParseError = true;
        _currentErrorKind = errorKind;

        if (OnParseError.HasDelegate)
        {
            InputParseException parseException = new(_editingValue, typeof(TValue), errorKind, ex);
            await OnParseError.InvokeAsync(parseException);
        }
    }

    #endregion

    #region Nested Types

    /// <summary>
    /// Internal exception used to distinguish validation failures from parse failures in the catch block.
    /// </summary>
    private sealed class InputFieldValidationException(InputFieldErrorKind errorKind, string message)
        : Exception(message)
    {
        public InputFieldErrorKind ErrorKind { get; } = errorKind;
    }

    #endregion
}