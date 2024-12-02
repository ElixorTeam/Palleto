using Microsoft.Extensions.Configuration;

namespace Pl.Shared.Web.Extensions;

public static class ConfigurationExtensions
{
    public static ConfigurationManager LoadAppSettings<T>(this ConfigurationManager manager)
    {
        Assembly assembly = typeof(T).Assembly;
        using Stream? appsettingsStream = assembly
            .GetManifestResourceStream($"{assembly.GetName().Name!}.appsettings.json");

        if (appsettingsStream == null) return manager;

        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonStream(appsettingsStream)
            .Build();

        manager.AddConfiguration(config);

        return manager;
    }

    public static T GetValueSafe<T>(this IConfigurationSection section, string key)
        => section.GetValue<T>(key) ?? throw new NotImplementedException();

    public static T GetValueOrDefault<T>(this IConfigurationSection section, string key, T defaultValue)
        => section.GetValue<T>(key) ?? defaultValue;
}