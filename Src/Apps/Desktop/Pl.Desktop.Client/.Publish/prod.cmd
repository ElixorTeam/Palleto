@echo off
dotnet publish ..\Pl.Desktop.Client.csproj --configuration ReleaseVS --framework net8.0-windows10.0.19041.0 --runtime win10-x64 /p:AppxPackageDir=\\palych\Install\Apps\ScalesDesktop\ /p:AppInstallerUri=\\palych\Install\Apps\ScalesDesktop && powershell -File "Utils\UpdateAppInstaller.ps1" -appInstallerFilePath "\\palych\Install\Apps\ScalesDesktop\ScalesDesktop_x64.appinstaller"
