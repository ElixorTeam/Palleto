using Phetch.Core;
using Pl.Tablet.Models;
using Pl.Tablet.Models.Features.Plus;

namespace Pl.Tablet.Client.Source.Shared.Api.Tablet.Endpoints;

public class PluEndpoints(ITabletApi tabletApi)
{
    public Endpoint<uint, PluDto> PluEndpoint { get; } = new(
        tabletApi.GetPluByNumber,
        options: new() { DefaultStaleTime = TimeSpan.FromHours(1) });
}