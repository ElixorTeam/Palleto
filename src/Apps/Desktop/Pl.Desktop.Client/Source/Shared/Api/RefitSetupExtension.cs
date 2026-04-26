namespace Pl.Desktop.Client.Source.Shared.Api;

internal static class RefitSetupExtension
{
    public static MauiAppBuilder RegisterRefitClients(this MauiAppBuilder builder)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        Type configurationType = typeof(IRefitClient);
        List<IRefitClient> configurations = assembly.GetTypes()
            .Where(t => configurationType.IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IRefitClient>()
            .ToList();

        foreach (IRefitClient config in configurations)
            config.Configure(builder);

        return builder;
    }
}