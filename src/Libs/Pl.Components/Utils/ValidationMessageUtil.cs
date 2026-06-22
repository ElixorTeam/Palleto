namespace Pl.Components;

/// <summary>
/// Helpers for formatting validation messages shown next to form fields.
/// </summary>
public static partial class ValidationMessageUtil
{
    [GeneratedRegex(@"^'[^']+'\s+", RegexOptions.CultureInvariant)]
    private static partial Regex PropertyNamePrefixRegex();

    /// <summary>
    /// Removes a leading FluentValidation property name prefix (e.g. <c>'Наименование' </c>)
    /// when the field label is already shown in the form.
    /// </summary>
    public static string FormatFieldError(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return message;

        string stripped = PropertyNamePrefixRegex().Replace(message, "");
        if (stripped.Length == 0 || stripped.Length == message.Length)
            return message;

        return char.ToUpper(stripped[0], CultureInfo.CurrentCulture) + stripped[1..];
    }
}
