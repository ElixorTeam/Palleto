namespace WsDataCore.Models;

public sealed class WsWeighingSettingsModel
{
    #region Public and private fields and properties

    private int KneadingMinValue => 1;
    private int KneadingMaxValue => 140;
    private int _kneading;
    public int Kneading
    {
        get => _kneading;
        set
        { 
            _kneading = Math.Min(Math.Max(value, KneadingMinValue), KneadingMaxValue);
        }
    }
    private byte LabelsCountMinValue => 1;
    private byte LabelsCountMaxValue => 130;
    private byte _labelsCountMain;
    public byte LabelsCountMain
    {
        get => _labelsCountMain;
        set
        {
            _labelsCountMain = Math.Min(Math.Max(value, LabelsCountMinValue), LabelsCountMaxValue);
        }
    }

    public WsWeighingSettingsModel()
    {
        Kneading = KneadingMinValue;
        LabelsCountMain = LabelsCountMinValue;
    }

    #endregion

    #region Public and private methods

    public string GetPrintDescription(string ip, string name,
        bool isConnected, int scaleCounter, int labelPrintedCount, byte labelCount) =>
        $"{name} | {ip} | " +
        $"{(isConnected ? "Подключен" : "Отключен")} | " +
        $"{WsLocaleCore.Table.LabelCounter}: {scaleCounter} | " +
        $"{WsLocaleCore.LabelPrint.Labels}: {labelPrintedCount} {WsLocaleCore.Strings.From} {labelCount}";

    #endregion
}