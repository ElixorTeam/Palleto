using Pl.Admin.Client.Source.Shared.Api.Labelary;
using Phetch.Core;
// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Api.Web.Endpoints;

public class ZplEndpoints(ILabelaryApi labelary)
{
    public Endpoint<ZplEndpointArgs, string> ZplEndpoint { get; } = new(
        async value =>
        {
            double widthInch = Math.Round(value.Width / 25.4, 2);
            double heightInch = Math.Round(value.Height / 25.4, 2);

            StringContent content = new(value.Zpl);
            content.Headers.ContentType = new("application/x-www-form-urlencoded");
            HttpResponseMessage response = await labelary.RenderZpl(widthInch, heightInch, content, value.RotateDegrees);

            await using MemoryStream ms = new();
            await response.Content.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        },
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}

public record ZplEndpointArgs(string Zpl, ushort Width, ushort Height, short RotateDegrees = 0);