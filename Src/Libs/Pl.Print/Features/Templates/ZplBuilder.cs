using Pl.Print.Features.Templates.Models;
using Pl.Print.Features.Templates.Utils;
using Pl.Print.Features.Templates.Variables;
using Scriban;
using Scriban.Runtime;

namespace Pl.Print.Features.Templates;

public static class ZplBuilder
{
    public static string GenerateZpl(PrintSettings settings, TemplateVars vars)
    {
        if (settings.Resources.Any(var => !var.Key.EndsWith("_sql")))
            throw new InvalidOperationException("All resource keys must end with '_sql'.");

        TemplateContext context = new() { StrictVariables = true };

        ScriptObject scriptObject1 = new();

        scriptObject1.Import(vars);
        scriptObject1.Import(settings.Resources);
        context.PushGlobal(scriptObject1);

        string zpl = Template.Parse(settings.Settings.Template).Render(context);

        return TemplateUtils.FormatComments(zpl);
    }
}