@echo off
setlocal enabledelayedexpansion

:: Проверка, передана ли версия в качестве аргумента
if "%~1"=="" (
    :: Запросить версию, если аргумент не передан
    set /p version="Enter version (e.g., 1.0.0): "
) else (
    :: Использовать переданную версию
    set "version=%~1"
)

:: Проверка, что версия была задана
if "%version%"=="" (
    echo No version provided. Exiting...
    exit /b 1
)

:: Выводим введенную версию
echo.
echo Version: %version%

:: Команда для публикации с .NET
dotnet publish .. -c DevelopVs -r win-x64 -o .\.publish --no-self-contained

:: Команда для упаковки с Velopack
vpk pack -v %version% -u ru.gap.palleto -p .\.publish --packTitle Palleto -e Pl.Desktop.Client.exe -o \\palych\Install\Tests\PalletoDev --packAuthors GapResurs,VladStandart --framework net9.0-x64-desktop,webview2

:: Удаление директории ./publish после выполнения
rd /s /q .\.publish