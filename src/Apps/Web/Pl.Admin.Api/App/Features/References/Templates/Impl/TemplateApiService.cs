using Pl.Admin.Api.App.Features.References.Templates.Common;
using Pl.Admin.Api.App.Features.References.Templates.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.Templates.Impl.Validators;
using Pl.Admin.Api.App.Shared.Enums;
using Pl.Database.Entities.Zpl.Templates;
using Pl.Admin.Api.App.Features.References.Templates.Impl.Extensions;
using Pl.Admin.Models.Features.References.Template.Commands;
using Pl.Admin.Models.Features.References.Template.Queries;
using Pl.Admin.Models.Features.References.Template.Universal;

namespace Pl.Admin.Api.App.Features.References.Templates.Impl;

internal sealed class TemplateApiService(
    WsDbContext dbContext,
    TemplateUpdateApiValidator updateValidator,
    TemplateCreateApiValidator createValidator
    ) : ITemplateService
{
    #region Queries

    public async Task<TemplateDto> GetByIdAsync(Guid id) =>
        TemplateExpressions.ToDto.Compile().Invoke(await dbContext.Templates.SafeGetById(id, FkProperty.Template));

    public Task<TemplateDto[]> GetAllAsync() => dbContext.Templates
        .AsNoTracking()
        .OrderBy(i => i.IsWeight)
        .ThenBy(i => i.Name)
        .Select(TemplateExpressions.ToDto)
        .ToArrayAsync();

    public async Task<ProxyDto[]> GetProxiesByIsWeightAsync(bool isWeight)
    {
        return await dbContext.Templates
            .AsNoTracking()
            .Where(i => i.IsWeight == isWeight)
            .OrderBy(i => i.Name)
            .Select(TemplateExpressions.ToProxy)
            .ToArrayAsync();
    }

    public async Task<TemplateBodyDto> GetBodyByIdAsync(Guid id)
    {
        TemplateEntity entity = await dbContext.Templates.SafeGetById(id, FkProperty.Template);
        return new()
        {
            Body = entity.Body
        };
    }

    public async Task<BarcodeItemWrapper> GetBarcodeTemplates(Guid id)
    {
        TemplateEntity entity = await dbContext.Templates.SafeGetById(id, FkProperty.Template);

        return new()
        {
            Top = entity.BarcodeTopBody.ToDto(),
            Bottom = entity.BarcodeBottomBody.ToDto(),
            Right = entity.BarcodeRightBody.ToDto()
        };
    }

    #endregion

    #region Commands

    public async Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto dto)
    {
        TemplateEntity entity = await updateValidator.ValidateAndGetAsync(dbContext.Templates, dto, id);

        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return TemplateExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<TemplateDto> CreateAsync(TemplateCreateDto dto)
    {
        await createValidator.ValidateAsync(dbContext.Templates, dto);

        TemplateEntity entity = dto.ToEntity();

        await dbContext.Templates.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return TemplateExpressions.ToDto.Compile().Invoke(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.Templates.SafeDeleteAsync(i => i.Id == id, FkProperty.Template);

    public async Task<BarcodeItemWrapper> UpdateBarcodeTemplates(Guid id, BarcodeItemWrapper barcodes)
    {
        ValidationResult result = await new BarcodeItemWrapperValidator().ValidateAsync(barcodes);

        if (!result.IsValid)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = "Ошибка в шк",
                StatusCode = HttpStatusCode.UnprocessableEntity
            };

        TemplateEntity entity = await dbContext.Templates.SafeGetById(id, FkProperty.Template);

        entity.BarcodeTopBody = barcodes.Top.ToItem();
        entity.BarcodeRightBody = barcodes.Right.ToItem();
        entity.BarcodeBottomBody = barcodes.Bottom.ToItem();

        await dbContext.SaveChangesAsync();

        return new()
        {
            Top = entity.BarcodeTopBody.ToDto(),
            Bottom = entity.BarcodeBottomBody.ToDto(),
            Right = entity.BarcodeRightBody.ToDto()
        };
    }

    #endregion
}