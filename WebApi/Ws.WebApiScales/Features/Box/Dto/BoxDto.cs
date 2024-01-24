﻿using System.Xml.Serialization;

namespace Ws.WebApiScales.Features.Box.Dto;

[Serializable]
public sealed class BoxDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;
    
    [XmlAttribute("Weight")]
    public decimal Weight { get; set; }
}