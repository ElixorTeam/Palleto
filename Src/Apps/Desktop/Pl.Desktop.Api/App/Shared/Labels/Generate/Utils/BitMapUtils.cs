using System.Drawing;
using System.Drawing.Imaging;
using Svg;

namespace Pl.Desktop.Api.App.Shared.Labels.Generate.Utils;

public static class BitMapUtils
{
    public static Bitmap ReadSvg(string svg, ushort rotate)
    {
        SvgDocument svgDocument = SvgDocument.FromSvg<SvgDocument>(svg);

        using MemoryStream memoryStream = new();

        svgDocument.Draw().Save(memoryStream, ImageFormat.Png);

        memoryStream.Seek(0, SeekOrigin.Begin);
        Bitmap map = new(memoryStream);

        if (rotate == 90)
            map.RotateFlip(RotateFlipType.Rotate270FlipNone);

        return map;
    }

    public static byte[] ToMonochromeBytes(Bitmap original)
    {
        Bitmap newBitmap = new(original.Width, original.Height, PixelFormat.Format32bppArgb);

        for (int y = 0 ; y < original.Height ; y++)
            for (int x = 0 ; x < original.Width ; x++)
            {
                Color pixelColor = original.GetPixel(x, y);
                Color newColor = pixelColor.A == 0 ?
                    Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);
                newBitmap.SetPixel(x, y, newColor);
            }

        using MemoryStream memoryStream = new();
        newBitmap.Save(memoryStream, ImageFormat.Png);

        return memoryStream.ToArray();
    }
}