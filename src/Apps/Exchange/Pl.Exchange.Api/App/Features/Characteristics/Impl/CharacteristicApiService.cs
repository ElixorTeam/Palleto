using Microsoft.EntityFrameworkCore;
using Pl.Database;
using Pl.Exchange.Api.App.Features.Characteristics.Common;
using Pl.Exchange.Api.App.Features.Characteristics.Impl.Models;

namespace Pl.Exchange.Api.App.Features.Characteristics.Impl;

using GroupedCharacteristicValidator=Models.GroupedCharacteristicValidator;

internal sealed partial class CharacteristicApiService(
    GroupedCharacteristicValidator validator, ILogger<CharacteristicApiService> logger,  WsDbContext dbContext) :
    BaseService<GroupedCharacteristic>(validator), ICharacteristicService
{
    public ResponseDto Load(HashSet<GroupedCharacteristic> dtos)
    {
        ResolveUniqueUidLocal(dtos);
        DeleteCharacteristics(dtos);

        ResolveUniqueLocal(
            dtos,
            dto => (dto.PluUid, dto.BoxUid, dto.BundleCount),
            "Характеристика - (Box, Plu, BundleCount) не уникальна"
        );

        FilterValidDtos(dtos);

        ResolveUniqueDb(dtos);
        ResolveIsWeightDb(dtos);

        ResolveNotExistsFkDb(dtos, dbContext.Plus, dto => dto.PluUid, "Плу - не найдена");
        ResolveNotExistsFkDb(dtos, dbContext.Boxes, dto => dto.BoxUid, "Коробка - не найдена");

        SaveCharacteristics(dtos);
        return OutputDto;
    }
}