// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Serialization;

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.QueryParameter, Namespace = "", IsNullable = false)]
public sealed class WsResponseQueryParameterModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlAttribute(nameof(Name))]
    public string Name { get; set; }
    [XmlAttribute(nameof(Value))]
    public string Value { get; set; }

    public WsResponseQueryParameterModel(string query, string value)
    {
        Name = query;
        Value = value;
    }

    public WsResponseQueryParameterModel(SqlParameter sqlParameter)
    {
        Name = sqlParameter.ParameterName;
        Value = sqlParameter.Value.ToString() ?? $"<{nameof(string.Empty)}>";
    }

    public WsResponseQueryParameterModel()
    {
        Name = string.Empty;
        Value = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Value)}: {Value}. ";

    #endregion
}