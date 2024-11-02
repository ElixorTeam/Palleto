using Microsoft.Build.Construction;

namespace Pl.Architecture.Tests.Utils;

public static class SolutionUtils
{
    private const string SolutionConfiguration = "Develop_x64";
    private const string SolutionFileName = "WeightService.sln";
    private const string SolutionRelativePath = $@"..\..\..\..\..\..\{SolutionFileName}";
    private static readonly SolutionFile SolutionFile = SolutionFile.Parse(Path.GetFullPath(SolutionRelativePath));

    # region Public

    public static TheoryData<string, Assembly> GetAllAssemblies() => GetAssemblies(FilterAllProjects);

    public static TheoryData<string, Assembly> GetFrontendAssemblies() => GetAssemblies(FilterFrontendProjects);

    public static TheoryData<string, Assembly> GetApiAssemblies() => GetAssemblies(FilterApiProjects);

    public static TheoryData<string, string> GetProjectFiles()
    {
        TheoryData<string, string> projects = [];

        foreach (ProjectInSolution project in SolutionFile.ProjectsInOrder)
        {
            if (!FilterAllProjects(project)) continue;
            projects.Add(Path.GetFileName(project.AbsolutePath), project.AbsolutePath);
        }
        return projects;
    }

    public static Assembly FindProjectAssembly(string projectName)
    {
        ProjectInSolution? project = SolutionFile.ProjectsInOrder.FirstOrDefault(p =>
            string.Equals(Path.GetFileNameWithoutExtension(p.AbsolutePath), projectName, StringComparison.OrdinalIgnoreCase));
        if (project == null) throw new FileNotFoundException($"Project {projectName} not found in solution.");

        string assemblyPath = FindAssemblyPath(project.AbsolutePath);
        return Assembly.LoadFrom(assemblyPath);
    }

    # endregion

    # region Private

    private static bool FilterAllProjects(ProjectInSolution project) =>
        project.AbsolutePath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) &&
        !project.AbsolutePath.Contains("Test", StringComparison.OrdinalIgnoreCase);

    private static bool FilterFrontendProjects(ProjectInSolution project) =>
        FilterAllProjects(project) &&
        project.AbsolutePath.Contains("Apps", StringComparison.OrdinalIgnoreCase) &&
        project.AbsolutePath.Contains('.', StringComparison.OrdinalIgnoreCase) &&
        !project.AbsolutePath.Contains("Pl", StringComparison.OrdinalIgnoreCase) &&
        !project.AbsolutePath.Contains("Api", StringComparison.OrdinalIgnoreCase);

    private static bool FilterApiProjects(ProjectInSolution project) =>
        FilterAllProjects(project) && project.AbsolutePath.Contains(".Api", StringComparison.OrdinalIgnoreCase);

    private static string FindAssemblyPath(string projectPath)
    {
        string outputDirectory = Path.Combine(Path.GetDirectoryName(projectPath)!, "bin", SolutionConfiguration);
        string projectName = Path.GetFileNameWithoutExtension(projectPath).Replace(".csproj", "");
        return Directory.GetFiles(outputDirectory, $"{projectName}.dll", SearchOption.AllDirectories).First();
    }

    private static TheoryData<string, Assembly> GetAssemblies(Func<ProjectInSolution, bool> projectFilter)
    {
        TheoryData<string, Assembly> assemblies = new();

        foreach (ProjectInSolution project in SolutionFile.ProjectsInOrder)
        {
            if (!projectFilter(project)) continue;
            string assemblyPath = FindAssemblyPath(project.AbsolutePath);
            assemblies.Add(Path.GetFileNameWithoutExtension(project.AbsolutePath), Assembly.LoadFrom(assemblyPath));
        }

        return assemblies;
    }

    # endregion
}