using System.Net.NetworkInformation;

namespace Pl.Desktop.Client.Source.Shared.Extensions;

public static class WindowsExtensions
{
    public static string Format(this PhysicalAddress macAddress, char separator)
    {
        byte[] bytes = macAddress.GetAddressBytes();
        return string.Join(separator.ToString(), bytes.Select(b => b.ToString("X2")));
    }
}