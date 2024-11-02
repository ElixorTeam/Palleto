using Pl.Shared.Utils;

namespace Pl.Desktop.Client.Source.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceOrMock<TInterface, TImplementation, TMockImplementation>(
        this IServiceCollection services,
        bool useMock,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TInterface : class
        where TImplementation : TInterface
        where TMockImplementation : TInterface
    {
        Type implementationType = useMock && ConfigurationUtils.IsDevelop ? typeof(TMockImplementation) : typeof(TImplementation);

        services.Add(new(typeof(TInterface), implementationType, lifetime));

        return services;
    }
}