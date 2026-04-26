namespace Pl.Print.Features.Templates.Utils;

public static partial class TemplateUtils
{
    [GeneratedRegex(@"\{\*(.*?)\*\}")]
    private static partial Regex CommentsRegex();

    [GeneratedRegex(@"\{\{ ([a-zA-Z0-9_]+_sql) \}\}")]
    private static partial Regex ResourcesRegex();

    public static List<string> GetResourcesKeys(string template) =>
        ResourcesRegex()
            .Matches(template)
            .Select(match => match.Groups[1].Value).Distinct()
            .ToList();

    public static string FormatComments(string template) =>
        CommentsRegex().Replace(template, m => $"^FX {m.Groups[1].Value.Trim()}");

    public static string SetupPluStorageMethod(string template, string storageName) =>
        template.Replace("storage_method", $"{storageName.Transliterate().ToLower()}_sql");
}