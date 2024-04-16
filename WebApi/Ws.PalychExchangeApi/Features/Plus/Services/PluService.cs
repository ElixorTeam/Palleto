using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Plus.Common;
using Ws.PalychExchangeApi.Features.Plus.Dto;
using Ws.PalychExchangeApi.Features.Plus.Dto.PluDto;

namespace Ws.PalychExchangeApi.Features.Plus.Services;

internal sealed partial class PluService(PluDtoValidator validator) : BaseService<PluDto>(validator), IPluService
{
    public ResponseDto Load(PlusWrapper dtoWrapper)
    {
        dtoWrapper.Plus.RemoveAll(i => i.IsDelete);

        ResolveUniqueUidLocal(dtoWrapper.Plus);
        ResolveUniqueLocal(dtoWrapper.Plus, dto => dto.Number, "Номер (внутри запроса) - не уникален");

        List<PluDto> validDtos = FilterValidDtos(dtoWrapper.Plus);

        ResolveUniqueNumberDb(validDtos);

        ResolveNotExistsFkDb(validDtos, DbContext.Boxes, dto => dto.BoxUid, "Коробка - не найдена");
        ResolveNotExistsFkDb(validDtos, DbContext.Clips, dto => dto.ClipUid, "Клипса - не найдена");
        ResolveNotExistsFkDb(validDtos, DbContext.Brands, dto => dto.BrandUid, "Бренд - не найден");
        ResolveNotExistsFkDb(validDtos, DbContext.Bundles, dto => dto.BundleUid, "Пакет - не найден");

        SavePlus(validDtos);
        return OutputDto;
    }
}