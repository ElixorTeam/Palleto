using Pl.Exchange.Api.App.Features.Plus.Common;
using Pl.Exchange.Api.App.Features.Plus.Dto;

namespace Pl.Exchange.Api.App.Features.Plus.Impl;

internal sealed partial class PluApiService(PluDtoValidator validator, ILogger<PluApiService> logger) : BaseService<PluDto>(validator), IPluService
{
    public ResponseDto Load(HashSet<PluDto> dtos)
    {
        ResolveUniqueUidLocal(dtos);
        DeleteNestings(dtos);

        ResolveUniqueLocal(dtos, dto => dto.Number, "Номер (внутри запроса) - не уникален");

        FilterValidDtos(dtos);

        ResolveUniqueNumberDb(dtos);

        SetDefaultFk(dtos);

        ResolveNotExistsFkDb(dtos, DbContext.Boxes, dto => dto.BoxUid, "Коробка - не найдена");
        ResolveNotExistsFkDb(dtos, DbContext.Clips, dto => dto.ClipUid, "Клипса - не найдена");
        ResolveNotExistsFkDb(dtos, DbContext.Brands, dto => dto.BrandUid, "Бренд - не найден");
        ResolveNotExistsFkDb(dtos, DbContext.Bundles, dto => dto.BundleUid, "Пакет - не найден");

        SavePlus(dtos);
        return OutputDto;
    }
}