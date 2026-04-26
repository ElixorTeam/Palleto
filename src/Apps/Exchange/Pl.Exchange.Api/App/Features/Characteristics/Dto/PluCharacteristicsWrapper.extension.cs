using Pl.Exchange.Api.App.Features.Characteristics.Impl.Models;

namespace Pl.Exchange.Api.App.Features.Characteristics.Dto;

internal static class PluCharacteristicsWrapperExtensions
{
    internal static HashSet<GroupedCharacteristic> ToGrouped(this PluCharacteristicsWrapper dto)
    {
        return dto.PluCharacteristics.SelectMany(plu => plu.Characteristics
            .GroupBy(characteristic => new GroupedCharacteristic
            {
                PluUid = plu.Uid,
                IsDelete = characteristic.IsDelete,
                Name = characteristic.Name,
                BoxUid = characteristic.BoxUid,
                Uid = characteristic.Uid,
                BundleCount = characteristic.BundleCount
            })
            .Select(group => group.Key))
        .ToHashSet();
    }
}