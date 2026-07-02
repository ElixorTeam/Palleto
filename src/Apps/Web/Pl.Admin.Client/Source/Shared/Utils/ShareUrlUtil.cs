namespace Pl.Admin.Client.Source.Shared.Utils;

public static class ShareUrlUtil
{
    public static string MaxUrl(string link) =>
        $"https://max.ru/:share?text='{Uri.EscapeDataString(link)}'";

    public static string TgUrl(string link) =>
        $"https://t.me/share/url?url={Uri.EscapeDataString(link)}";
}
