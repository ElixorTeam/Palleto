param (
    [string]$appInstallerFilePath
)

$newSchema = "http://schemas.microsoft.com/appx/appinstaller/2018"

Write-Host "Loading file as text: " $localFilePath

$fileContent = Get-Content $appInstallerFilePath

$fileContent = $fileContent.Replace("http://schemas.microsoft.com/appx/appinstaller/2017/2", $newSchema);

$fileContent = $fileContent.Replace(
    'HoursBetweenUpdateChecks="0"',
    'HoursBetweenUpdateChecks="0" ShowPrompt="true" UpdateBlocksActivation="true"');

$fileContent = $fileContent.Replace(
    '</UpdateSettings>',
        '<ForceUpdateFromAnyVersion>true</ForceUpdateFromAnyVersion>
        <AutomaticBackgroundTask />
    </UpdateSettings>
      ');

Write-Host "New file contents: $fileContent";

if ($fileContent -like "*UpdateBlocksActivation*" -and $fileContent -like "*$newSchema*")
{
    Write-Host "Replaced namespace (xmlns) with newest schema version. Modified UpdateSettings."
    $fileContent | Set-Content -Path $appInstallerFilePath -Encoding UTF8
}
else
{
    Write-Host "Text replacement failed."
    exit 1
}