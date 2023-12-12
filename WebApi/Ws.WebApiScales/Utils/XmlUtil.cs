﻿using System.Xml;
using System.Xml.Serialization;

namespace Ws.WebApiScales.Utils;

public static class XmlUtil
{
    public static string SerializeToXml<T>(T obj)
    {
        XmlSerializerNamespaces namespaces = new();
        namespaces.Add(string.Empty, string.Empty);
    
        using StringWriter stringWriter = new();
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new() { OmitXmlDeclaration = true });
    
        new XmlSerializer(typeof(T)).Serialize(xmlWriter, obj, namespaces);
    
        return stringWriter.ToString();
    }

}