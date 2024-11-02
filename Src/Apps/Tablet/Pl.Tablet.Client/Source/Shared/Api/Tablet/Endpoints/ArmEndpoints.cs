using Phetch.Core;
using Pl.Tablet.Models;
using Pl.Tablet.Models.Features.Arms;

namespace Pl.Tablet.Client.Source.Shared.Api.Tablet.Endpoints;

public class ArmEndpoints(ITabletApi tabletApi)
{
    public ParameterlessEndpoint<ArmDto> ArmEndpoint { get; } = new(
        tabletApi.GetCurrentArm,
        options: new() { DefaultStaleTime = TimeSpan.FromHours(1) });
}