using Pl.Architecture.Tests.Extensions;

namespace Pl.Architecture.Tests.Tests;

public class StructureTests
{
    public static TheoryData<string, Assembly> TestData => SolutionUtils.GetAllAssemblies();

    [Theory]
    [MemberData(nameof(TestData))]
    public void Default_Features_Dependencies(string projectName, Assembly project)
    {
        Types? types = Types.InAssembly(project);
        types.CheckFeatures($"{projectName}");
        types.CheckFeatures($"{projectName}.App");
        types.CheckFeatures($"{projectName}.Source");
    }
}