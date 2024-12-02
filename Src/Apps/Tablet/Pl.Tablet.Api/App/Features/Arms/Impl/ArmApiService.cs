using Pl.Tablet.Api.App.Features.Arms.Common;
using Pl.Tablet.Api.App.Shared.Helpers;
using Pl.Tablet.Models.Features.Arms;

namespace Pl.Tablet.Api.App.Features.Arms.Impl;

internal sealed class ArmApiService(UserHelper userHelper) : IArmService
{
    #region Queries

    public ArmDto GetCurrent()
    {
        return new()
        {
            Id = userHelper.UserId,
            Name = "Тестовая линия",
            WarehouseName = "Тестовый склад"
        };
    }

    #endregion

}