namespace Pl.Exchange.Api.App.Shared.Dto;

[Serializable]
public sealed record ResponseSuccesses(Guid Uid)
{
    [XmlAttribute("Guid")]
    public Guid Uid = Uid;

    public ResponseSuccesses() : this(Guid.Empty)
    {
        // DON'T TOUCH NEED FOR SERIALIZATION
    }
}