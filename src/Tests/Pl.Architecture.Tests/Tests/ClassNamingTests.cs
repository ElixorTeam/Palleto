namespace Pl.Architecture.Tests.Tests;

public class ClassNamingTests
{
    public static TheoryData<string, Assembly> TestData => SolutionUtils.GetAllAssemblies();

    [Theory]
    [MemberData(nameof(TestData))]
    public void All_Enums_Must_End_On_Type(string _, Assembly project)
    {
        Types? types = Types.InAssembly(project);

        TestResult? result = types
            .That()
            .AreEnums()
            .Should()
            .HaveNameEndingWith("Type")
            .GetResult();

        result.FailingTypes.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void All_Interfaces_Must_Start_On_I(string _, Assembly project)
    {
        Types? types = Types.InAssembly(project);

        TestResult? result = types
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        result.FailingTypes.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void All_Base_Classes_Must_Be_Abstract(string _, Assembly project)
    {
        Types? types = Types.InAssembly(project);

        TestResult? result = types
            .That()
            .AreClasses().And().AreNotStatic()
            .And()
            .HaveNameStartingWith("Base").Or().HaveNameEndingWith("Base")
            .Should()
            .BeAbstract()
            .GetResult();

        result.FailingTypes.Should().BeEmpty();
    }
}