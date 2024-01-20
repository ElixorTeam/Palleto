﻿using System.Xml.Serialization;
using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Features.Brand.Dto;

[XmlRoot("Brands")]
public class BrandsWrapper : BaseWrapper
{
    [XmlElement("Brand")]
    public List<BrandDto> Brands { get; set; } = [];
}