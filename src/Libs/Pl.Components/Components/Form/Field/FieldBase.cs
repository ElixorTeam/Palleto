using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;

namespace Pl.Components.Components;

public abstract class FieldBase : ComponentBase, IDisposable
{
    private FieldIdentifier? _fieldIdentifier;
    private LambdaExpression? _cachedExpression;
    private EditContext? _subscribedEditContext;

    [CascadingParameter]
    private EditContext? CascadedEditContext { get; set; }

    /// <summary>
    /// Gets or sets additional CSS classes applied to the outer Field container.
    /// </summary>
    [Parameter]
    public string Class { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the label text displayed above or beside the control.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the helper text displayed below the control.
    /// </summary>
    /// <remarks>
    /// Hidden when the field is in an error state; the error message takes its place.
    /// </remarks>
    [Parameter]
    public string? HelperText { get; set; }

    /// <summary>
    /// Gets or sets a manual error text to display on the field.
    /// </summary>
    /// <remarks>
    /// When set, the field enters an invalid state and displays this text as the error message.
    /// This can be used to inject validation errors from external sources (e.g., server-side
    /// validation or <see cref="PlDynamicForm"/>). When the field also has EditContext validation
    /// errors, this text takes precedence over the auto-generated validation messages.
    /// </remarks>
    [Parameter]
    public string? ErrorText { get; set; }

    /// <summary>
    /// Gets or sets the ARIA label for the control.
    /// </summary>
    [Parameter]
    public string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets the orientation of the field layout.
    /// </summary>
    [Parameter]
    public FieldOrientation Orientation { get; set; } = FieldOrientation.Vertical;

    /// <summary>
    /// The auto-generated ID for the inner control element.
    /// </summary>
    protected string ControlId { get; } = $"formfield-{Guid.NewGuid():N}";

    /// <summary>
    /// The auto-generated ID for the description element.
    /// </summary>
    protected string DescriptionId => $"{ControlId}-description";

    /// <summary>
    /// The auto-generated ID for the error element.
    /// </summary>
    protected string ErrorId => $"{ControlId}-error";

    /// <summary>
    /// Gets whether the field currently has errors from the EditContext.
    /// </summary>
    protected bool HasEditContextErrors => EditContextErrors.Any();

    /// <summary>
    /// Gets the validation error messages from the EditContext.
    /// </summary>
    protected IEnumerable<string> EditContextErrors
    {
        get
        {
            if (CascadedEditContext is not null && _fieldIdentifier.HasValue)
                return CascadedEditContext.GetValidationMessages(_fieldIdentifier.Value);

            return [];
        }
    }

    /// <summary>
    /// Gets whether the field is in an invalid state. Subclasses may override
    /// to add additional error sources (e.g., parse errors).
    /// </summary>
    protected virtual bool IsInvalid => HasEditContextErrors || !string.IsNullOrEmpty(ErrorText);

    /// <summary>
    /// Gets the value for the <c>aria-describedby</c> attribute. Points to
    /// the error element when invalid, or the description element when helper text
    /// is present. Subclasses may override for custom logic.
    /// </summary>
    protected virtual string DescribedById => IsInvalid ? ErrorId
        : !string.IsNullOrEmpty(HelperText) ? DescriptionId : string.Empty;

    /// <summary>
    /// Returns the lambda expression for the bound field, used to create
    /// a <see cref="FieldIdentifier"/> for EditContext integration.
    /// </summary>
    protected abstract LambdaExpression? GetFieldExpression();

    /// <summary>
    /// Notifies the cascaded <see cref="EditContext"/> that the field value has changed,
    /// triggering validation. Call this after updating the bound value.
    /// </summary>
    protected void NotifyFieldChanged()
    {
        if (CascadedEditContext is not null && _fieldIdentifier.HasValue)
            CascadedEditContext.NotifyFieldChanged(_fieldIdentifier.Value);
    }

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

        LambdaExpression? expression = GetFieldExpression();
        if (CascadedEditContext is not null && expression is not null)
        {
            // Only recompute when the expression reference changes to avoid
            // Expression.Compile() on every render cycle.
            if (ReferenceEquals(expression, _cachedExpression))
                return;
            _cachedExpression = expression;
            _fieldIdentifier = CreateFieldIdentifier(expression);
        }
        else
        {
            _cachedExpression = null;
            _fieldIdentifier = null;
        }
    }

    private static FieldIdentifier CreateFieldIdentifier(LambdaExpression expression)
    {
        switch (expression.Body)
        {
            case MemberExpression memberExpression:
            {
                object model = EvaluateExpression(memberExpression.Expression!);
                return new(model, memberExpression.Member.Name);
            }
            case UnaryExpression { Operand: MemberExpression innerMember }:
            {
                object model = EvaluateExpression(innerMember.Expression!);
                return new(model, innerMember.Member.Name);
            }
            default:
                throw new ArgumentException("The provided expression must be a member access expression.", nameof(expression));
        }
    }

    private static object EvaluateExpression(Expression expression) =>
        Expression.Lambda<Func<object>>(Expression.Convert(expression, typeof(object))).Compile()();

    private void OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e) =>
        StateHasChanged();

    public void Dispose()
    {
        if (_subscribedEditContext is not null)
            _subscribedEditContext.OnValidationStateChanged -= OnValidationStateChanged;
        GC.SuppressFinalize(this);
    }
}