using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Pl.Desktop.Client.Source.Shared.Utils;

public record NetworkInfo(IPAddress Ip, PhysicalAddress Mac, string Type);

public static class WindowsUtils
{
    [Pure]
    public static string GetHost() => Dns.GetHostName();

    [Pure]
    public static Guid GetBiosId()
    {
        try
        {
            ManagementObjectSearcher searcher = new("SELECT UUID FROM Win32_ComputerSystemProduct");
            ManagementObject? queryObj = searcher.Get().OfType<ManagementObject>().FirstOrDefault();
            if (queryObj?["UUID"] == null) return Guid.Empty;
            string uuid = queryObj["UUID"].ToString() ?? string.Empty;
            return Guid.TryParse(uuid, out Guid guid) ? guid : Guid.Empty;
        }
        catch
        {
            return Guid.Empty;
        }
    }

    [Pure]
    public static NetworkInfo GetNetworkInfo()
    {
        string type = "None";
        IPAddress ip = IPAddress.None;
        PhysicalAddress mac = PhysicalAddress.None;

        try
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            NetworkInterface? activeInterface = networkInterfaces.FirstOrDefault(nic =>
                nic.OperationalStatus == OperationalStatus.Up &&
                nic.NetworkInterfaceType != NetworkInterfaceType.Loopback);

            if (activeInterface != null)
            {
                mac = activeInterface.GetPhysicalAddress();

                ip = activeInterface.GetIPProperties()
                    .UnicastAddresses
                    .FirstOrDefault(i =>
                        i.Address.AddressFamily == AddressFamily.InterNetwork
                    )?.Address ?? IPAddress.None;

                type = activeInterface.NetworkInterfaceType switch
                {
                    NetworkInterfaceType.Wireless80211 => "Wi-Fi",
                    NetworkInterfaceType.Ethernet => "Ethernet",
                    _ => type
                };
            }
        }
        catch
        {
            // pass;
        }
        return new(ip, mac, type);
    }
}