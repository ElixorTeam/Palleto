using Pl.Database.Entities.Ref1C.Characteristics;

namespace Pl.Exchange.Api.App.Features.Characteristics.Impl.Models;

public record GroupedCharacteristic : BaseDto
{
    public required Guid PluUid { get; set; }
    public required Guid BoxUid { get; set; }
    public required short BundleCount { get; set; }
    public required bool IsDelete { get; set; }
    public required string Name { get; set; } = string.Empty;

    public CharacteristicEntity ToEntity(DateTime dateTime) => new()
    {
        Id = Uid,
        Name = Name,
        BundleCount = BundleCount,
        PluId = PluUid,
        BoxId = BoxUid,
        ChangeDt = dateTime,
        CreateDt = dateTime
    };
}