using Pl.Admin.Api.App.Features.References.TemplateResources.Common;
using Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Validators;
using Pl.Admin.Api.App.Shared.Enums;
using Svg;
using Pl.Database.Entities.Zpl.ZplResources;
using Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Extensions;
using Pl.Admin.Models.Features.References.TemplateResources.Commands;
using Pl.Admin.Models.Features.References.TemplateResources.Queries;

namespace Pl.Admin.Api.App.Features.References.TemplateResources.Impl;

internal sealed class ZplResourceApiService(
    WsDbContext dbContext,
    ZplResourceUpdateApiValidator updateValidator,
    ZplResourceCreateApiValidator createValidator
    ) : IZplResourceService
{
    #region Queries

    public async Task<TemplateResourceDto> GetByIdAsync(Guid id) =>
        ZplResourceExpressions.ToDto.Compile().Invoke(await dbContext.ZplResources.SafeGetById(id, FkProperty.ZplResource));

    public Task<TemplateResourceDto[]> GetAllAsync() => dbContext.ZplResources
        .AsNoTracking()
        .OrderBy(i => i.Type)
        .ThenBy(i => i.Name)
        .Select(ZplResourceExpressions.ToDto)
        .ToArrayAsync();

    public async Task<TemplateResourceBodyDto> GetBodyByIdAsync(Guid id)
    {
        ZplResourceEntity entity = await dbContext.ZplResources.SafeGetById(id, FkProperty.ZplResource);
        return new()
        {
            Body = entity.Zpl
        };
    }

    #endregion

    #region Commands

    public async Task<TemplateResourceDto> UpdateAsync(Guid id, ZplResourceUpdateDto dto)
    {
        ZplResourceEntity entity =  await updateValidator.ValidateAndGetAsync(dbContext.ZplResources, dto, id);

        ValidateSvg(dto.Body, entity.Type);

        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return ZplResourceExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<TemplateResourceDto> CreateAsync(ZplResourceCreateDto dto)
    {
        await createValidator.ValidateAsync(dbContext.ZplResources, dto);

        ValidateSvg(dto.Body, dto.Type);

        ZplResourceEntity entity = dto.ToEntity();

        await dbContext.ZplResources.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return ZplResourceExpressions.ToDto.Compile().Invoke(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.ZplResources.SafeDeleteAsync(i => i.Id == id, FkProperty.ZplResource);

    #endregion

    private static void ValidateSvg(string svg, ZplResourceType type)
    {
        if (type == ZplResourceType.Text) return;
        try
        {
            SvgDocument.FromSvg<SvgDocument>(svg);
        }
        catch
        {
            throw new ApiInternalException
            {
                ErrorDisplayMessage = "Body not valid",
                StatusCode = HttpStatusCode.UnprocessableEntity
            };
        }
    }
}