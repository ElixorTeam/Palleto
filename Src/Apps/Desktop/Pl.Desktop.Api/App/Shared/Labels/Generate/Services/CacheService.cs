using System.Drawing;
using BinaryKits.Zpl.Label.Elements;
using Microsoft.Extensions.Localization;
using Pl.Database;
using Pl.Database.Entities.Zpl.Templates;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Utils;
using Pl.Desktop.Api.App.Shared.Labels.Localization;
using Pl.Print.Features.Barcodes.Models;
using Pl.Print.Features.Templates.Models;
using Pl.Print.Features.Templates.Utils;
using Pl.Shared.Enums;

namespace Pl.Desktop.Api.App.Shared.Labels.Generate.Services;

public partial class CacheService(WsDbContext dbContext, IStringLocalizer<LabelGenResources> localizer)
{
    [GeneratedRegex(",([^,]+),")]
    private static partial Regex MyRegex();

    public TemplateInfo GetTemplateByUidFromCacheOrDb(Guid templateUid, string pluStorageName)
    {
        TemplateEntity? temp = dbContext.Templates.Find(templateUid);
        if (temp is null)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = localizer["TemplateNotFound"],
            };

        return new(
            TemplateUtils.SetupPluStorageMethod(temp.Body, pluStorageName),
            (ushort)temp.Width,
            (ushort)temp.Height,
            (ushort)temp.Rotate,
            temp.BarcodeTopBody.ConvertAll(i => new BarcodeVar(i.Property, i.Format)),
            temp.BarcodeRightBody.ConvertAll(i => new BarcodeVar(i.Property, i.Format)),
            temp.BarcodeBottomBody.ConvertAll(i => new BarcodeVar(i.Property, i.Format))
        );

        // IEasyCachingProvider easyCachingProvider;
        // string zplKey = $"TEMPLATES:{templateUid}";
        // if (easyCachingProvider.Exists(zplKey))
        //     return easyCachingProvider.Get<TemplateFromCache>(zplKey).Value;
        // easyCachingProvider.Set($"{zplKey}", tempFromCache, TimeSpan.FromHours(1));
    }

    public Dictionary<string, string> GetResourcesFromCacheOrDb(List<string> resourcesName, ushort rotate)
    {
        // HashSet<string> resourceNameUniq = resourcesName.ToHashSet();
        // Dictionary<string, string?> cached = redisCachingProvider
        //     .HMGet($"ZPL_RESOURCES:{rotate}", resourceNameUniq.ToList());
        //
        // bool allValuesNonNull = cached != null && cached.Values.All(value => value != null);
        //
        // if (allValuesNonNull)
        //     return cached!;

        // IRedisCachingProvider redisCachingProvider;

        Dictionary<string, string?> cached = dbContext.ZplResources.ToDictionary(
            i => $"{i.Name.ToLower()}_sql",
            i =>
            {
                if (i.Type == ZplResourceType.Text)
                    return i.Zpl;

                Bitmap resourceData = BitMapUtils.ReadSvg(i.Zpl, rotate);
                byte[] resourceBytes = BitMapUtils.ToMonochromeBytes(resourceData);
                ZplDownloadGraphics resourceZ64 = new('x', "x", resourceBytes, ZplCompressionScheme.Z64);
                string zpl = resourceZ64.ToZplString().Replace("~DGx:x.GRF", "").Replace("\n", "");

                Match match = MyRegex().Match(zpl);

                if (!match.Success)
                    throw new NotImplementedException();

                string firstValue = match.Groups[1].Value;
                return $"^GFA,{firstValue}{zpl}";
            })!;

        // redisCachingProvider.HMSet($"ZPL_RESOURCES:{rotate}", cached, TimeSpan.FromHours(1));

        return cached!;
    }
}