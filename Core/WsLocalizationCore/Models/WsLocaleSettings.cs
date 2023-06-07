// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocaleSettings : WsLocaleBase
{
    #region Public and private fields, properties, constructor

    public string AllowedHosts => Lang == WsEnumLanguage.English ? "Allowed hosts" : "Разрешенные хосты";
    public string SectionRowsCount => Lang == WsEnumLanguage.English ? "Section's rows count" : "Количество строк в секции";
    public string ItemRowsCount => Lang == WsEnumLanguage.English ? "Records's rows count" : "Количество строк в записи";
    public string SectionAndItemRowsCount => Lang == WsEnumLanguage.English ? "Section's and record's rows count" : "Количество строк в секции и записи";
    public string SelectTopRowsCount => Lang == WsEnumLanguage.English ? "Selection's top rows count" : "Количество строк выборки";
    public string Version => Lang == WsEnumLanguage.English ? "Version of the json-settings file" : "Версия файла json-настроек";

    #endregion
}