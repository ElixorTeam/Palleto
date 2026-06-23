namespace Pl.Components;

public enum ButtonType
{
    Submit,
    Reset,
    Button
}

internal static class ButtonTypeExtensions
{
    internal static string HtmlType(this ButtonType type) =>
        type switch
        {
            ButtonType.Submit => "submit",
            ButtonType.Reset => "reset",
            _ => "button"
        };
}