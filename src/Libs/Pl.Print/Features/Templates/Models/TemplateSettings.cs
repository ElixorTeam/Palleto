namespace Pl.Print.Features.Templates.Models;

public record PrintSettings(
    TemplateInfo Settings,
    Dictionary<string, string> Resources
);