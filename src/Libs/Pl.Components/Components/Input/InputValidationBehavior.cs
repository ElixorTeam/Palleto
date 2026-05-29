using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;

namespace Pl.Components.Components;

/// <summary>
/// Encapsulates shared EditContext validation logic used by all input components.
/// Used via composition since input components inherit directly from ComponentBase.
/// </summary>
internal sealed class InputValidationBehavior
{
    private FieldIdentifier _fieldIdentifier;
    private EditContext? _editContext;

    /// <summary>
    /// Gets whether the field has validation errors in the current EditContext.
    /// </summary>
    public bool IsInvalid => _editContext?.GetValidationMessages(_fieldIdentifier).Any() == true;

    /// <summary>
    /// Gets the effective name attribute, falling back to the FieldIdentifier name when inside an EditForm.
    /// </summary>
    public string? GetEffectiveName(string? name) =>
        name ?? (_editContext != null ? _fieldIdentifier.FieldName : null);

    /// <summary>
    /// Gets the effective aria-invalid value combining manual AriaInvalid, parent field state, and EditContext validation.
    /// </summary>
    public string? GetEffectiveAriaInvalid(bool? ariaInvalid, bool? fieldIsInvalid)
    {
        if (ariaInvalid.HasValue)
            return ariaInvalid.Value ? "true" : "false";
        if (fieldIsInvalid == true || IsInvalid)
            return "true";
        return null;
    }

    /// <summary>
    /// Notifies the EditContext that the field value has changed, triggering validation.
    /// </summary>
    public void NotifyFieldChanged()
    {
        _editContext?.NotifyFieldChanged(_fieldIdentifier);
    }

    /// <summary>
    /// Updates the EditContext and FieldIdentifier from cascading parameters.
    /// Call from <see cref="Microsoft.AspNetCore.Components.ComponentBase.OnParametersSet"/>.
    /// </summary>
    public void Update<T>(EditContext? cascadedEditContext, Expression<Func<T>>? valueExpression)
    {
        if (cascadedEditContext == null || valueExpression == null)
            return;
        _editContext = cascadedEditContext;
        _fieldIdentifier = FieldIdentifier.Create(valueExpression);
    }
}