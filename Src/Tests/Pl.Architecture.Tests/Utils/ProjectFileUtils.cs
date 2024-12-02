using System.Xml.Linq;

namespace Pl.Architecture.Tests.Utils;

public static class ProjectFileUtils
{
    public static bool CheckPrivateProject(string projectFilePath, string dependencyProject)
    {
        XDocument doc = XDocument.Load(projectFilePath);
        XElement? project = doc.Element("Project");

        if (project == null) throw new InvalidOperationException("Invalid project file format");

        List<XElement> dependencies = project.Descendants("ProjectReference").ToList();

        List<XElement> dependenciesInclude =
            dependencies.Where(i =>
                i.Attribute("Include") != null &&
                i.Attribute("Include")!.Value.Contains(dependencyProject, StringComparison.InvariantCultureIgnoreCase)
                ).ToList();

        if (dependenciesInclude.Count == 0) return true;

        IEnumerable<XElement> dependenciesAll =
            dependenciesInclude.Where(i =>
                i.Attribute("PrivateAssets") != null &&
                i.Attribute("PrivateAssets")!.Value.Contains("All", StringComparison.InvariantCultureIgnoreCase)
                );

        return dependenciesAll.Any();
    }
}