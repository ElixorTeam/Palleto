using Pl.Shared.Enums;

namespace Pl.Shared.Utils;

public static class ConfigurationUtils
{
    [Pure]
    public static bool IsDevelop => Config switch
    {
        ConfigurationType.Develop => true,
        ConfigurationType.Release => false,
        _ => throw new ArgumentOutOfRangeException(nameof(IsDevelop), IsDevelop.ToString())
    };

    [Pure]
    public static ConfigurationType Config =>
#if RELEASE
        ConfigurationType.Release;
#else
        ConfigurationType.Develop;
#endif
}
