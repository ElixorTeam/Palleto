@echo off
dotnet publish ..\Pl.Desktop.Client.csproj --configuration DevelopVS --framework net8.0-windows10.0.19041.0 --runtime win10-x64 /p:AppxPackageDir=\\palych\Install\VSSoft\ScalesVs\ /p:AppInstallerUri=\\palych\Install\VSSoft\ScalesVs\ && powershell -File "Utils\UpdateAppInstaller.ps1" -appInstallerFilePath "\\palych\Install\VSSoft\ScalesVs\ScalesDesktop_x64.appinstaller"
