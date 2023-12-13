﻿using FluentValidation.Results;
using Ws.StorageCore.Entities.SchemaDiag.LogsWebs;
using Ws.StorageCore.Entities.SchemaRef1c.Boxes;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.WebApiScales.Dto.PluCharacteristic;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Utils;

namespace Ws.WebApiScales.Services;

public class PluCharacteristicService(ResponseDto responseDto, IHttpContextAccessor httpContextAccessor)
{
    private readonly SqlPluNestingFkRepository _pluNestingFkRepository = new();

    public ActionResult<ResponseDto> LoadCharacteristics(PluCharacteristicsDto pluCharacteristics)
    {
        
        DateTime requestTime = DateTime.Now;
        string currentUrl = httpContextAccessor.HttpContext?.Request.Path ?? string.Empty; 
        
        SqlPluRepository pluRepository = new();

        IOrderedEnumerable<PluCharacteristicDto> pluCharacteristicDtos = 
            pluCharacteristics.Characteristics.OrderBy(item => item.PluGuid);

        foreach (PluCharacteristicDto pluCharacteristicDto in pluCharacteristicDtos)
        {
            SqlPluEntity pluDb = pluRepository.GetByUid1C(pluCharacteristicDto.PluGuid);

            if (pluCharacteristicDto.IsMarked)
            {
                SetCharacteristicIsMarked(pluDb, pluCharacteristicDto);
                continue;
            }
            
            ValidationResult validationResult = new PluCharacteristicDtoValidator().Validate(pluCharacteristicDto);

            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                responseDto.AddError(pluCharacteristicDto.Guid, string.Join(" | ", errors));
                continue;
            }
            
            if (IsPluValid(pluDb, pluCharacteristicDto) == false) 
                continue;
            
            PluCharacteristicSaveOrUpdate(pluDb, pluCharacteristicDto);
        }
        
        new SqlLogWebRepository().Save(requestTime,   
        XmlUtil.SerializeToXml(pluCharacteristics),   
        XmlUtil.SerializeToXml(responseDto), currentUrl, responseDto.SuccessesCount, responseDto.ErrorsCount);
        
        return responseDto;
    }
    
    private void SetCharacteristicIsMarked(SqlPluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        SqlPluNestingFkEntity pluNestingFkDefault = _pluNestingFkRepository.GetDefaultByPlu(plu);
        
        if (pluNestingFkDefault.IsExists && pluNestingFkDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} | {plu.Name} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
        
        SqlPluNestingFkEntity nesting = _pluNestingFkRepository.GetByPluAndUid1C(plu, pluCharacteristicDto.Guid);
        if (nesting.IsNew)
        {
            responseDto.AddSuccess(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} | {plu.Name} - вложенность {pluCharacteristicDto.AttachmentsCountAsInt} не найдена для удаления!");
            return;
        }
   
        nesting.IsMarked = true;
        SqlCoreHelper.Instance.Update(nesting);
        responseDto.AddSuccess(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} | {plu.Name} - вложенность {pluCharacteristicDto.AttachmentsCountAsInt} удалена!");
    }
    
    private bool IsPluValid(SqlPluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        if (plu.IsNew)
        {
            responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатуры {pluCharacteristicDto.Name} не найдено!");
            return false;
        }

        if (!plu.IsCheckWeight)
            return true;
        
        responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} | {plu.Name} - весовая");
        return false;
    }

    private void PluCharacteristicSaveOrUpdate(SqlPluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        SqlPluNestingFkEntity pluNestingFkDefault = _pluNestingFkRepository.GetDefaultByPlu(plu);
        
        if (pluNestingFkDefault.IsExists && pluNestingFkDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} | {plu.Name} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
        
        SqlPluNestingFkEntity nesting = _pluNestingFkRepository.GetByPluAndUid1C(plu, pluCharacteristicDto.Guid);
        SqlBoxEntity boxDb = new SqlBoxRepository().GetItemByUid1C(new("71bc8e8a-99cf-11ea-a220-a4bf0139eb1b"));
        if (boxDb.IsNew)
        {
            responseDto.AddError(pluCharacteristicDto.Guid, "Невозможно установить коробку");
            return;
        }
        
        nesting.Plu = plu;
        nesting.Box = boxDb;
        nesting.IsDefault = false;
        nesting = pluCharacteristicDto.AdaptTo(nesting);
        
        SqlCoreHelper.Instance.SaveOrUpdate(nesting);
        responseDto.AddSuccess(pluCharacteristicDto.Guid, $"Номенклатура: {plu.Number} | {plu.Name}  / Удалить {pluCharacteristicDto.IsMarked} / AttachmentsCount {nesting.BundleCount}");
    }
}