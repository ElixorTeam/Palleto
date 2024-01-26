﻿using System.Xml.Serialization;
using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Features.Brands.Dto;

[XmlRoot("Brands")]
internal sealed class BrandsWrapper : BaseWrapper
{
    [XmlElement("Brand")]
    public List<BrandDto> Brands { get; set; } = [];
}