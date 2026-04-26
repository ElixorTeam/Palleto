using Pl.Desktop.Models.Features.Pallets.Output;

namespace Pl.Desktop.Client.Source.Shared.Api.Desktop.Endpoints;

public class PalletEndpoints(IDesktopApi desktopApi)
{
    public Endpoint<PiecePalletsArgs, PalletDto[]> PiecePalletsEndpoint { get; } = new(
        value => desktopApi.GetPalletsByArm(value.StartDt, value.EndDt),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<LabelEndpointArgs, LabelDto[]> PalletLabelsEndpoint { get; } = new(
        value => desktopApi.GetPalletLabels(value.PalletUid),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(30) });

    public Endpoint<PiecePalletsNumberArgs, PalletDto[]> PiecePalletsNumberEndpoint { get; } = new(
        value => desktopApi.GetPalletByNumber(value.Number),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) }
    );

    public void InsertPiecePallet(PiecePalletsArgs args, PalletDto data) =>
        PiecePalletsEndpoint.UpdateQueryData(args, q =>
        {
            if (q.Data == null) return q.Data!;
            IEnumerable<PalletDto> newData = q.Data.Prepend(data);
            return newData.ToArray();
        });
}

public record LabelEndpointArgs(Guid PalletUid);

public record PiecePalletsArgs(DateTime? StartDt, DateTime? EndDt);

public record PiecePalletsNumberArgs(uint Number);