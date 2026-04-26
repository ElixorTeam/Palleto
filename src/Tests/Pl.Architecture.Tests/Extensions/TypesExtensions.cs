namespace Pl.Architecture.Tests.Extensions;

public static class TypesExtensions
{
    public static void CheckFeatures(this Types types, string path)
    {
        TestResult? result = types
            .Slice()
            .ByNamespacePrefix($"{path}.Features")
            .Should()
            .NotHaveDependenciesBetweenSlices()
            .GetResult();

        result.FailingTypes.Should().BeEmpty();
    }
}