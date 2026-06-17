using System.Collections;
using TailwindMerge;

namespace Pl.Components;

public static partial class CssUtil
{
    private static readonly TwMerge TwMerge = new();
    private static readonly Regex ValidClassNameRegex = new(@"^[a-zA-Z0-9_\-:/.[\]()%!@#&>+~=*,' ]+$", RegexOptions.Compiled);
    private static readonly char[] WhitespaceSeparators = [' ', '\t', '\n', '\r'];

    /// <summary>
    /// Combines multiple class names intelligently, handling Tailwind CSS conflicts.
    /// Equivalent to shadcn cn() utility.
    /// </summary>
    public static string Cn(params object?[] inputs)
    {
        if (inputs.Length == 0)
            return string.Empty;

        List<string> classes = [];

        foreach (object? input in inputs)
            ProcessInput(input, classes);

        if (classes.Count == 0)
            return string.Empty;
        return TwMerge.Merge(string.Join(" ", classes)) ?? string.Empty;
    }

    /// <summary>
    /// Recursively processes input values and extracts class names.
    /// </summary>
    private static void ProcessInput(object? input, List<string> classes)
    {
        switch (input)
        {
            case null:
                return;
            case string str:
            {
                if (string.IsNullOrWhiteSpace(str))
                    return;
                string[] parts = str.Split(WhitespaceSeparators, StringSplitOptions.RemoveEmptyEntries);
                classes.AddRange(parts.Where(IsValidClassName));
                return;
            }
            // Note: In C#, `condition && "class"` returns "class" or false, not true/false
            case bool:
                return;
        }

        if (input is IEnumerable enumerable and not string) {
            foreach (object? item in enumerable)
                ProcessInput(item, classes);
            return;
        }

        string strValue = input.ToString() ?? string.Empty;
        if (IsValidClassName(strValue))
            classes.Add(strValue);
    }

    /// <summary>
    /// Validates that a CSS class name contains only safe characters.
    /// Rejects classes that could be used for CSS injection attacks.
    /// </summary>
    private static bool IsValidClassName(string className)
    {
        if (string.IsNullOrWhiteSpace(className) || className.Length > 200)
            return false;

        if (className.Contains("expression", StringComparison.OrdinalIgnoreCase) ||
            className.Contains("javascript", StringComparison.OrdinalIgnoreCase) ||
            className.Contains("url(", StringComparison.OrdinalIgnoreCase) ||
            className.Contains("import", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return ValidClassNameRegex.IsMatch(className);
    }
}