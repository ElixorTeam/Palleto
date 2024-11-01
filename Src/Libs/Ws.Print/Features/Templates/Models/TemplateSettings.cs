namespace Ws.Print.Features.Templates.Models;

public record PrintSettings(
    TemplateInfo Settings,
    Dictionary<string, string> Resources
);