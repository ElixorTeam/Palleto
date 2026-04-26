using Pl.Admin.Api.App.Features.References1C.Plus.Common;
using Pl.Admin.Api.App.Features.References1C.Plus.Impl.Expressions;
using Pl.Admin.Api.App.Shared.Enums;
using Pl.Database.Entities.Ref1C.Characteristics;
using Pl.Database.Entities.Ref1C.Nestings;
using Pl.Database.Entities.Ref1C.Plus;
using Pl.Admin.Models.Features.References1C.Plus.Commands.Update;
using Pl.Admin.Models.Features.References1C.Plus.Queries;

namespace Pl.Admin.Api.App.Features.References1C.Plus.Impl;

internal sealed class PluApiService(WsDbContext dbContext) : IPluService
{
    #region Queries

    public async Task<PluDto> GetByIdAsync(Guid id)
    {
        PluEntity entity = await dbContext.Plus.SafeGetById(id, FkProperty.Plu);
        await LoadDefaultForeignKeysAsync(entity);
        return PluExpressions.ToDto.Compile().Invoke(entity);
    }

    public Task<PluDto[]> GetAllAsync()
    {
        return dbContext.Plus
            .AsNoTracking()
            .OrderBy(i => i.Number)
            .Select(PluExpressions.ToDto)
            .ToArrayAsync();
    }

    public async Task<CharacteristicDto[]> GetCharacteristics(Guid id)
    {
        PluEntity plu = await dbContext.Plus.SafeGetById(id, FkProperty.Plu);

        await dbContext.Entry(plu).Reference(e => e.Clip).LoadAsync();
        await dbContext.Entry(plu).Reference(e => e.Bundle).LoadAsync();

        NestingEntity? nesting = await dbContext.Nestings
            .Include(i => i.Box)
            .SingleOrDefaultAsync(i => i.Id == id);

        List<CharacteristicDto> characteristics = [];

        CharacteristicPackageDto bundle = new(plu.Bundle.Id, plu.Bundle.Weight);
        CharacteristicPackageDto clip = new(plu.Clip.Id, plu.Clip.Weight);

        if (nesting != null)
            characteristics.Add(new()
            {
                Name = "По умолчанию",
                Count = (ushort)nesting.BundleCount,
                PluWeight = plu.Weight,
                Bundle = bundle,
                Clip = clip,
                Box = new(nesting.Box.Id, nesting.Box.Weight)
            });

        if (plu.IsWeight)
            return characteristics.ToArray();

        List<CharacteristicEntity> characteristicsEntities = await dbContext.Characteristics
            .Include(i => i.Box)
            .Where(i => i.PluId == id).ToListAsync();

        characteristics.AddRange(
        characteristicsEntities.Select(characteristic => new CharacteristicDto
        {
            Name = characteristic.Name,
            Count = (ushort)characteristic.BundleCount,
            PluWeight = plu.Weight,
            Bundle = bundle,
            Clip = clip,
            Box = new(characteristic.Box.Id, characteristic.Box.Weight)
        }));

        return characteristics.ToArray();
    }

    #endregion

    #region Commands

    public async Task<PluDto> Update(Guid id, PluUpdateDto dto)
    {
        PluEntity plu = await dbContext.Plus.SafeGetById(id, FkProperty.Plu);

        await dbContext.Templates.SafeExistAsync(i => i.Id == dto.TemplateId && i.IsWeight == plu.IsWeight, FkProperty.Template);

        plu.TemplateId = dto.TemplateId;
        await LoadDefaultForeignKeysAsync(plu);
        await dbContext.SaveChangesAsync();

        return PluExpressions.ToDto.Compile().Invoke(plu);
    }

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(PluEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.Clip).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Brand).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Bundle).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Template).LoadAsync();
    }

    #endregion
}