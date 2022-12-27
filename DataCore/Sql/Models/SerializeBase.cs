﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Formatters.Binary;
using DataCore.Enums;
using System.ComponentModel;

namespace DataCore.Sql.Models;


public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}

[Serializable]
public class SerializeBase : ISerializable
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Contrsuctor.
    /// </summary>
    public SerializeBase()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected SerializeBase(SerializationInfo info, StreamingContext context)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        //
    }

    public virtual XmlReaderSettings GetXmlReaderSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document,
    };

    public virtual XmlWriterSettings GetXmlWriterSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document,
        OmitXmlDeclaration = false,
        Indent = true,
        IndentChars = "\t",
    };

    # region Serialize
    public virtual string SerializeAsJson() => JsonConvert.SerializeObject(this);

    public virtual string SerializeByMemoryStream<T>() where T : new()
    {
        MemoryStream memoryStream = new();
        IFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(memoryStream, this);
        string result;
        using StreamReader streamReader = new(memoryStream);
        memoryStream.Position = 0;
        result = streamReader.ReadToEnd();
        memoryStream.Close();
        return result;
    }

    public virtual string SerializeAsXmlString<T>(bool isAddEmptyNamespace, bool isUtf16) where T : new()
    {
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];

        using var stringWriter =  isUtf16? new StringWriter() : new Utf8StringWriter();

        switch (isAddEmptyNamespace)
        {
            case true:
                {
                    XmlSerializerNamespaces emptyNamespaces = new();
                    emptyNamespaces.Add(string.Empty, string.Empty);
                    using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, GetXmlWriterSettings());
                    xmlSerializer.Serialize(xmlWriter, this, emptyNamespaces);
                    xmlWriter.Flush();
                    xmlWriter.Close();
                    break;
                }
            default:
                xmlSerializer.Serialize(stringWriter, this);
                break;
        }
        return stringWriter.ToString();
    }

    public virtual string SerializeAsHtml() => @$"
<html>
<body>
    {this}
</body>
</html>
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public virtual string SerializeAsText() => ToString();

    public virtual XmlDocument SerializeAsXmlDocument<T>(bool isAddEmptyNamespace, bool isUtf16) where T : new()
    {
        XmlDocument xmlDocument = new();
        string xmlString = SerializeAsXmlString<T>(isAddEmptyNamespace, false);
        byte[] bytes = isUtf16 ? Encoding.Unicode.GetBytes(xmlString) : Encoding.UTF8.GetBytes(xmlString);
        using MemoryStream memoryStream = new(bytes);
        memoryStream.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);
        xmlDocument.Load(memoryStream);
        return xmlDocument;
    }

    #endregion

    #region Deserialize

    public virtual T DeserializeFromXml<T>(string xml) where T : new()
    {
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        return (T)xmlSerializer.Deserialize(new MemoryStream(Encoding.Unicode.GetBytes(xml)));
    }

    public virtual T DeserializeFromXmlVersion2<T>(string xml) where T : new()
    {
        // Don't use it.
        // XmlSerializer xmlSerializer = new(typeof(T));
        // Use it.
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        using TextReader reader = new StringReader(xml);
        return (T)xmlSerializer.Deserialize(reader);
    }

    public virtual T DeserializeFromMemoryStream<T>(MemoryStream memoryStream) where T : new()
    {
	    memoryStream.Position = 0;
		IFormatter formatter = new BinaryFormatter();
        memoryStream.Seek(0, SeekOrigin.Begin);
        object? obj = formatter.Deserialize(memoryStream);
        return obj is null ? new() : (T)obj;
    }

    #endregion

    #region ContentResult
    public virtual ContentResult GetContentResultCore(FormatType formatType, object content, HttpStatusCode statusCode) => new()
    {
        ContentType = DataUtils.GetContentType(formatType),
        StatusCode = (int)statusCode,
        Content = content as string ?? content.ToString()
    };

    public virtual ContentResult GetContentResultCore(string formatString, object content, HttpStatusCode statusCode) =>
        GetContentResultCore(DataUtils.GetFormatType(formatString), content, statusCode);

    public virtual ContentResult GetContentResult(FormatType formatType, object content, HttpStatusCode statusCode) =>
        GetContentResultCore(formatType, content, statusCode);

    public virtual ContentResult GetContentResult(string formatString, object content, HttpStatusCode statusCode) =>
        GetContentResultCore(formatString, content, statusCode);

    public virtual ContentResult GetContentResult<T>(FormatType formatType, HttpStatusCode statusCode) where T : new() => formatType switch
    {
        FormatType.Text => GetContentResult(formatType, SerializeAsText(), statusCode),
        FormatType.JavaScript => GetContentResult(formatType, SerializeAsText(), statusCode),
        FormatType.Json => GetContentResult(formatType, SerializeAsJson(), statusCode),
        FormatType.Html => GetContentResult(formatType, SerializeAsHtml(), statusCode),
        FormatType.Xml or FormatType.XmlUtf8 => GetContentResult(formatType, SerializeAsXmlString<T>(true, false), statusCode),
        FormatType.XmlUtf16 => GetContentResult(formatType, SerializeAsXmlString<T>(true, true), statusCode),
        _ => throw DataUtils.GetArgumentException(nameof(formatType)),
    };

    public virtual ContentResult GetContentResult<T>(string formatString, HttpStatusCode statusCode) where T : new() =>
        GetContentResult<T>(GetFormatType(formatString), statusCode);

    #endregion

    public virtual FormatType GetFormatType(string formatType) => formatType.ToUpper() switch
    {
        "TEXT" => FormatType.Text,
        "JAVASCRIPT" => FormatType.JavaScript,
        "JSON" => FormatType.Json,
        "HTML" => FormatType.Html,
        "XML" or "" or "XMLUTF8" => FormatType.Xml,
        "XMLUTF16" => FormatType.XmlUtf16,
        _ => throw DataUtils.GetArgumentException(nameof(formatType)),
    };

    public virtual T ObjectFromDictionary<T>(IDictionary<string, object> dict) where T : new()
    {
        Type type = typeof(T);
        T result = (T)Activator.CreateInstance(type);
        foreach (KeyValuePair<string, object> item in dict)
        {
            type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
        }
        return result;
    }

    public virtual IDictionary<string, object> ObjectToDictionary<T>(T item) where T : new()
    {
        IDictionary<string, object> result = new Dictionary<string, object>();
        if (item is null)
            return result;
        object[] indexer = Array.Empty<object>();
        foreach (PropertyInfo info in item.GetType().GetProperties())
        {
            object value = info.GetValue(item, indexer);
            result.Add(info.Name, value);
        }
        return result;
    }

    public virtual XDocument GetBtXmlNamedSubString<T>(T item, XName name, object value) where T : new()
    {
        IDictionary<string, object> dict = ObjectToDictionary(item);
        XDocument result = new(
            new XElement("XMLScript", new XAttribute("Version", "2.0"),
                new XElement("Command",
                    new XElement("Print",
                        new XElement("Format", new XAttribute(name, value)),
                        dict.Select(x => new XElement("NameSubString",
                            new XAttribute("Key", x.Key),
                            new XElement("Value", x.Value)))
                    ))));
        return result;
    }

    public virtual string GetContent<T>(FormatType formatType) where T : new()
    {
        return formatType switch
        {
            FormatType.Text => SerializeAsText(),
            FormatType.JavaScript => XmlUtils.GetPrettyXmlOrJson(SerializeAsJson()),
            FormatType.Json => XmlUtils.GetPrettyXmlOrJson(SerializeAsJson()),
            FormatType.Html => SerializeAsHtml(),
            FormatType.Xml or FormatType.XmlUtf8 => XmlUtils.GetPrettyXml(SerializeAsXmlString<T>(true, false)),
            FormatType.XmlUtf16 => XmlUtils.GetPrettyXml(SerializeAsXmlString<T>(true, true)),
            _ => throw DataUtils.GetArgumentException(nameof(formatType)),
        };
    }

    #endregion

    #region Public and private methods - Properties

    public virtual object? GetPropertyDefaultValue(string name)
	{
		AttributeCollection? attributes = TypeDescriptor.GetProperties(this)[name]?.Attributes;
		Attribute? attribute = attributes?[typeof(DefaultValueAttribute)];
		if (attribute is DefaultValueAttribute defaultValueAttribute)
			return defaultValueAttribute.Value;
		return null;
	}

	public virtual string GetPropertyDefaultValueAsString(string name) =>
		GetPropertyDefaultValue(name)?.ToString() ?? string.Empty;

	public virtual int GetPropertyDefaultValueAsInt(string name) =>
		GetPropertyDefaultValue(name) is int value ? value : default;

	public virtual bool GetPropertyDefaultValueAsBool(string name) =>
		GetPropertyDefaultValue(name) is bool value ? value : default;

	public virtual IEnumerable<string> GetPropertiesNames() => 
		(from PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(this) select propertyDescriptor.Name).ToList();

	#endregion
}