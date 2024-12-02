using Pl.Tablet.Models.Features.Plus;

namespace Pl.Tablet.Models.Api;

public interface ITabletPluApi
{
    [Get("/plus")]
    Task<PluDto> GetPluByNumber(uint number);
}