using Pl.Tablet.Models.Features.Arms;

namespace Pl.Tablet.Api.App.Features.Arms.Common;

public interface IArmService
{
    #region Queries

    ArmDto GetCurrent();

    #endregion
}