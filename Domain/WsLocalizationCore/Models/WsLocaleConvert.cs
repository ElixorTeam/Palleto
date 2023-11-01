namespace WsLocalizationCore.Models;

public sealed class WsLocaleConvert : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public string BoolToString(bool isFlag) => isFlag ? Lang == WsEnumLanguage.English ? "yes" : "да" : Lang == WsEnumLanguage.English ? "no" : "нет";
    public string ByteToString(byte isFlag, string yes, string no) => isFlag == 0x01 ? yes : no;

    #endregion
}