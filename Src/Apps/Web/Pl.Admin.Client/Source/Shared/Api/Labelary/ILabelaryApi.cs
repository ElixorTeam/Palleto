using Refit;

namespace Pl.Admin.Client.Source.Shared.Api.Labelary;

public interface ILabelaryApi
{
    [Headers("Content-Type: application/x-www-form-urlencoded", "Accept: image/png")]
    [Post("/v1/printers/12dpmm/labels/{widthInch}x{heightInch}/0/")]
    Task<HttpResponseMessage> RenderZpl(double widthInch, double heightInch, [Body] HttpContent content, [Header("X-Rotation")] short rotate = 0);
}