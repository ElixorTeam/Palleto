﻿using System.Xml.Serialization;

namespace Ws.WebApiScales.Dto;

[Serializable]
public class ResponseSuccesses
{
    [XmlAttribute("Guid")]
    public Guid Guid { get; set; }
    
    [XmlAttribute("Message")]
    public string Message { get; set; } = string.Empty;
}