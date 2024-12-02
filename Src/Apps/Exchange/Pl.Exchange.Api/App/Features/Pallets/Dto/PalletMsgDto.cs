namespace Pl.Exchange.Api.App.Features.Pallets.Dto;

[XmlRoot(ElementName = "ResponseType")]
public class PalletMsgWrapper
{
    [XmlElement(ElementName = "Status")]
    public PalletUpdateStatus Status { get; set; } = new();
}

public class PalletUpdateStatus
{
    [XmlAttribute(AttributeName = "IsSuccess")]
    public bool IsSuccess { get; set; }

    [XmlAttribute(AttributeName = "Message")]
    public string Message { get; set; } = string.Empty;
}