using Phetch.Core;
using Pl.Admin.Models;
using Pl.Admin.Models.Features.Print.Labels;
using Pl.Admin.Models.Features.Print.Pallets;

// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Api.Web.Endpoints;

public class OperationsEndpoints(IWebApi webApi)
{
    public Endpoint<Guid, LabelDto> LabelEndpoint { get; } = new(
        webApi.GetLabelByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<Guid, ZplDto> LabelZplEndpoint { get; } = new(
        webApi.GetLabelZplByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<Guid, PalletDto> PalletEndpoint { get; } = new(
        webApi.GetPalletById,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, LabelPalletDto[]> PalletLabelsEndpoint { get; } = new(
        webApi.GetPalletLabels,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });
}