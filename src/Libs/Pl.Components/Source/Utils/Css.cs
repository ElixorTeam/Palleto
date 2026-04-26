namespace Pl.Components.Source.Utils;

public static class Css
{
    [Pure]
    public static string Class(params string?[] classes) =>
        string.Join(" ", classes).Trim();

    [Pure]
    public static string Class(params (string?, bool)[] classes) =>
        string.Join(" ", classes.Where(x => x.Item2).Select(x => x.Item1)).Trim();

    [Pure]
    public static string Class(string? always, params (string?, bool)[] classes) =>
        string.Join(" ", classes.Where(x => x.Item2).Select(x => x.Item1).Prepend(always)).Trim();
}