using Pl.Database.Entities.Ref1C.Nestings;
using Pl.Database.Entities.Ref1C.Plus;

namespace Pl.Exchange.Api.App.Features.Plus.Dto;

internal static class PluDtoExtensions
{
    internal static PluEntity ToPluEntity(this PluDto dto, DateTime dateTime) =>
        new()
        {
            Id = dto.Uid,
            IsWeight = dto.IsWeight,
            Ean13 = dto.Ean13,
            Itf14 = dto.Itf14,
            Name = dto.Name,
            FullName = dto.FullName,
            Description = dto.Description,
            Number = dto.Number,
            ShelfLifeDays = dto.ShelfLifeDays,
            BrandId = dto.BrandUid,
            ClipId = dto.ClipUid,
            BundleId = dto.BundleUid,
            Weight = dto.Weight,
            StorageMethod = dto.StorageMethod,
            ChangeDt = dateTime,
            CreateDt = dateTime
        };

    internal static NestingEntity ToNestingEntity(this PluDto dto, DateTime dateTime) =>
        new()
        {
            BoxId = dto.BoxUid,
            BundleCount = dto.BundleCount,
            CreateDt = dateTime,
            ChangeDt = dateTime
        };
}