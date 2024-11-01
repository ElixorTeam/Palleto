using Ws.Database.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Extensions;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Validators;
using Ws.DeviceControl.Api.App.Shared.Enums;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl;

internal sealed class ProductionSiteApiService(
    WsDbContext dbContext,
    UserHelper userHelper,
    ProductionSiteCreateApiValidator createValidator,
    ProductionSiteUpdateApiValidator updateValidator
    ) : IProductionSiteService
{
    #region Queries

    public async Task<ProxyDto> GetProxyByUserAsync()
    {
        ProxyDto? data = await userHelper.GetUserProductionSiteAsync();
        if (data == null) throw new ApiInternalException
        {
            ErrorDisplayMessage = "Площадка не установлена",
            StatusCode = HttpStatusCode.NotFound
        };
        return data;
    }

    public async Task<ProxyDto[]> GetProxiesAsync()
    {
        bool seniorSupport = await userHelper.ValidatePolicyAsync(PolicyEnum.SeniorSupport);
        if (seniorSupport)
        {
            bool developer = await userHelper.ValidatePolicyAsync(PolicyEnum.Developer);
            return await dbContext.ProductionSites
                .AsNoTracking()
                .IfWhere(!developer, entity => entity.Id != DefaultTypes.GuidMax)
                .OrderBy(i => i.Name)
                .Select(CommonExpressions.ProductionSite)
                .ToArrayAsync();
        }
        ProxyDto? userProductionSite = await userHelper.GetUserProductionSiteAsync();
        return userProductionSite != null ? [userProductionSite] : [];
    }

    public async Task<ProductionSiteDto> GetByIdAsync(Guid id) =>
        ProductionSiteExpressions.ToDto.Compile().Invoke(await dbContext.ProductionSites.SafeGetById(id, FkProperty.Label));

    public Task<ProductionSiteDto[]> GetAllAsync() => dbContext.ProductionSites
        .AsNoTracking()
        .OrderBy(i => i.Name)
        .Select(ProductionSiteExpressions.ToDto)
        .ToArrayAsync();

    #endregion

    #region Commands

    public async Task<ProductionSiteDto> CreateAsync(ProductionSiteCreateDto dto)
    {
        await createValidator.ValidateAsync(dbContext.ProductionSites, dto);

        ProductionSiteEntity entity = dto.ToEntity();

        await dbContext.ProductionSites.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return ProductionSiteExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<ProductionSiteDto> UpdateAsync(Guid id, ProductionSiteUpdateDto dto)
    {
        ProductionSiteEntity entity = await updateValidator.ValidateAndGetAsync(dbContext.ProductionSites, dto, id);
        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return ProductionSiteExpressions.ToDto.Compile().Invoke(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.ProductionSites.SafeDeleteAsync(i => i.Id == id, FkProperty.ProductionSite);

    #endregion
}