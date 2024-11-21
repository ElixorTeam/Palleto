using Phetch.Core;
using Pl.Admin.Models;
using Pl.Admin.Models.Features.References1C.Brands;
using Pl.Admin.Models.Features.References1C.Plus.Queries;
// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Api.Web.Endpoints;

public class References1CEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<PluDto[]> PlusEndpoint { get; } = new(
        webApi.GetPlus,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, PluDto> PluEndpoint { get; } = new(
        webApi.GetPluByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public ParameterlessEndpoint<PackageDto[]> BoxesEndpoint { get; } = new(
        webApi.GetBoxes,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, PackageDto> BoxEndpoint { get; } = new(
        webApi.GetBoxByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public ParameterlessEndpoint<PackageDto[]> BundlesEndpoint { get; } = new(
        webApi.GetBundles,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, PackageDto> BundleEndpoint { get; } = new(
        webApi.GetBundleByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public ParameterlessEndpoint<PackageDto[]> ClipsEndpoint { get; } = new(
        webApi.GetClips,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, PackageDto> ClipEndpoint { get; } = new(
        webApi.GetClipByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public ParameterlessEndpoint<BrandDto[]> BrandsEndpoint { get; } = new(
        webApi.GetBrands,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, BrandDto> BrandEndpoint { get; } = new(
        webApi.GetBrandByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, CharacteristicDto[]> CharacteristicsEndpoint { get; } = new(
        webApi.GetPluCharacteristics,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void UpdatePlu(PluDto plu)
    {
        PlusEndpoint.UpdateQueryData(new(),
            query => query.Data == null ? [plu] : query.Data.ReplaceItemBy(plu, p => p.Id == plu.Id).ToArray());
        PluEndpoint.UpdateQueryData(new(), _ => plu);
    }
}