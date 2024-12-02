using Pl.Tablet.Models.Features.Plus;

namespace Pl.Tablet.Api.App.Features.Plus.Common;

public interface IPluService
{
    #region Queries

    PluDto GetByNumber(uint number);

    #endregion
}