// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Utils;

public static class WsAssemblyUtils
{
    #region Public and private methods
    
    public static string GetAppVersion(Assembly executingAssembly)
    {
        FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
        string result = fieVersionInfo.FileVersion;
        if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
            result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
        return result;
    }
    
    public static string GetClickOnceNetworkInstallDirectory()
    {
        string? directory = null;
        if (ApplicationDeployment.IsNetworkDeployed)
            directory = Path.GetDirectoryName(ApplicationDeployment.CurrentDeployment.UpdateLocation.AbsolutePath);
        if (directory is not null && directory.StartsWith("\\") && !directory.StartsWith("\\\\"))
            directory = string.Join("\\", directory);
        return directory ?? "This application is not deployed using ClickOnce!";
    }
    
    public static string GetRunDirectory()
    {
        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        UriBuilder uri = new(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path) ?? string.Empty;
    }
    
    public static string GetLibVersion()
    {
        FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        string result = fieVersionInfo.FileVersion;
        if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
            result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
        return result;
    }

    #endregion
}