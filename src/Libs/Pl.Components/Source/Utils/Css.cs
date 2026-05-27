namespace Pl.Components.Source.Utils;

public static class Css
{
    [Pure]
    public static string Class(params string?[] classes) =>
        string.Join(" ", classes).Trim();
}