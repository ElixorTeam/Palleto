using Microsoft.Extensions.Configuration;
using Pl.Shared.Web.Extensions;
using Velopack;

namespace Pl.Desktop.Client.Source.Shared.Services.System;

public class AppService
{
    private readonly UpdateManager _updateManager;

    public AppService(IConfiguration configuration)
    {
        IConfigurationSection deploymentSection = configuration.GetRequiredSection("Deployment");
        _updateManager = new(deploymentSection.GetValueSafe<string>("urlOrPath"));
    }

    public string Version => _updateManager.CurrentVersion?.ToNormalizedString() ?? "Unknown";

    public async Task Update()
    {
        if (!_updateManager.IsInstalled) return;

        UpdateInfo? newVersion = await _updateManager.CheckForUpdatesAsync();
        if (newVersion == null)
            return;

        await _updateManager.DownloadUpdatesAsync(newVersion);

        _updateManager.ApplyUpdatesAndRestart(newVersion);
    }
}