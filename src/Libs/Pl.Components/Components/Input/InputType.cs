namespace Pl.Components.Components;

public enum InputType
{
    /// <summary>
    /// Single-line text input (default).
    /// Accepts any text characters.
    /// </summary>
    Text,

    /// <summary>
    /// Email address input.
    /// Provides email validation and @ key on mobile keyboards.
    /// </summary>
    Email,

    /// <summary>
    /// Password input with obscured characters.
    /// Text is hidden for security (displayed as dots or asterisks).
    /// </summary>
    Password,

    /// <summary>
    /// Telephone number input.
    /// Optimized for phone number entry with tel: keyboard layout.
    /// </summary>
    Tel,

    /// <summary>
    /// URL input.
    /// Provides URL validation and .com key on mobile keyboards.
    /// </summary>
    Url,

    /// <summary>
    /// Search query input.
    /// Displays search icon and may show recent searches.
    /// </summary>
    Search,
}

internal static class InputTypeExtensions
{
    internal static string GetHtmlType(this InputType type) =>
        type switch
        {
            InputType.Text => "text",
            InputType.Email => "email",
            InputType.Password => "password",
            InputType.Tel => "tel",
            InputType.Url => "url",
            InputType.Search => "search",
            _ => "text"
        };
}