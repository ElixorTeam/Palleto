namespace Pl.Exchange.Api.App.Shared.Dto;

[Serializable]
public record ResponseError(Guid Uid, string Message)
{
    [XmlAttribute("Guid")]
    public Guid Uid = Uid;

    [XmlAttribute("Message")]
    public string Message = Message;

    public ResponseError() : this(Guid.Empty, string.Empty)
    {
        // DON'T TOUCH NEED FOR SERIALIZATION
    }
}