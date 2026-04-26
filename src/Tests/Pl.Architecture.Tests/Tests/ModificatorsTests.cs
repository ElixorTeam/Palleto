namespace Pl.Architecture.Tests.Tests;

public class ModificatorsTests
{
    public static TheoryData<string, Assembly> AllAssemblies => SolutionUtils.GetAllAssemblies();
    public static TheoryData<string, Assembly> ApiAssemblies => SolutionUtils.GetApiAssemblies();

    [Theory]
    [MemberData(nameof(AllAssemblies))]
    public void All_Utils_Classes_Should_Be_Static(string _, Assembly project)
    {
        TestResult? result = Types.InAssembly(project)
            .That()
            .HaveNameEndingWith("Utils")
            .Should()
            .BeStatic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(ApiAssemblies))]
    public void All_ApiServices_Should_Be_Sealed_And_Internal(string _, Assembly project)
    {
        TestResult? result = Types.InAssembly(project)
            .That()
            .AreClasses()
            .And()
            .HaveNameEndingWith("ApiService")
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .And()
            .BeInternal()
            .GetResult();

        result.FailingTypes.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(ApiAssemblies))]
    public void All_Controllers_Should_Be_Sealed_And_Public(string _, Assembly project)
    {
        TestResult? result = Types.InAssembly(project)
            .That()
            .AreClasses()
            .And()
            .HaveNameEndingWith("Controller")
            .Should()
            .BeSealed()
            .And()
            .BePublic()
            .GetResult();

        result.FailingTypes.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(AllAssemblies))]
    public void All_Helpers_Should_Be_Sealed(string _, Assembly project)
    {
        TestResult? result = Types.InAssembly(project)
            .That()
            .AreClasses()
            .And()
            .HaveNameEndingWith("Helper")
            .Should()
            .BeSealed()
            .GetResult();

        result.FailingTypes.Should().BeEmpty();
    }

    [Fact]
    public void All_EfMappings_Should_Be_Sealed_And_Internal()
    {
        TestResult? result = Types.InAssembly(SolutionUtils.FindProjectAssembly("Pl.Database"))
            .That()
            .AreClasses()
            .And()
            .HaveNameEndingWith("MapConfig")
            .Should()
            .BeSealed()
            .And()
            .BeInternal()
            .GetResult();

        result.FailingTypes.Should().BeEmpty();
    }
}