namespace Pl.Architecture.Tests.Tests;

public class FsdTests
{
    public static TheoryData<string, Assembly> TestData => SolutionUtils.GetFrontendAssemblies();

    [Theory]
    [MemberData(nameof(TestData))]
    public void Shared_Should_HaveNoDependencies(string projectName, Assembly project)
    {
        Types types = Types.InAssembly(project);

        TestResult result = types
            .That()
            .ResideInNamespace($"{projectName}.Source.Shared")
            .ShouldNot()
            .HaveDependencyOnAll($"{projectName}.Source.Pages")
            .Or()
            .HaveDependencyOnAll($"{projectName}.Source.Widgets")
            .Or()
            .HaveDependencyOnAll($"{projectName}.Source.Features")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Features_Should_OnlyReference_Shared(string projectName, Assembly project)
    {
        Types types = Types.InAssembly(project);

        TestResult result = types
            .That()
            .ResideInNamespace($"{projectName}.Source.Features")
            .ShouldNot()
            .HaveDependencyOnAll($"{projectName}.Source.Pages")
            .Or()
            .HaveDependencyOnAll($"{projectName}.Source.Widgets")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Widgets_Should_OnlyReference_Shared_And_Features(string projectName, Assembly project)
    {
        Types types = Types.InAssembly(project);

        TestResult result = types
            .That()
            .ResideInNamespace($"{projectName}.Source.Widgets")
            .ShouldNot()
            .HaveDependencyOnAll($"{projectName}.Source.Pages")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}