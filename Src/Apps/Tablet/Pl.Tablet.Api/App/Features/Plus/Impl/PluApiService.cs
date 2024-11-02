using Pl.Tablet.Api.App.Features.Plus.Common;
using Pl.Tablet.Models.Features.Plus;

namespace Pl.Tablet.Api.App.Features.Plus.Impl;

internal sealed class PluApiService : IPluService
{
    #region Queries

    public PluDto GetByNumber(uint number)
    {
        List<PluDto> plus =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Number = 1111,
                Name = "Свинина"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Number = 2222,
                Name = "Курица"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Number = 3333,
                Name = "Телятина"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Number = 4444,
                Name = "Говядина"
            },
        ];

        return plus.Find(i => i.Number == number) ?? throw new ApiInternalException
        {
            ErrorDisplayMessage = "Плу не найден",
            StatusCode = HttpStatusCode.NotFound
        };
    }

    #endregion
}