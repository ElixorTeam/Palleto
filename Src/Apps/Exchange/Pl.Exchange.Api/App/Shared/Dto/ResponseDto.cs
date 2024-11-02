namespace Pl.Exchange.Api.App.Shared.Dto;

[XmlRoot("Response")]
public sealed class ResponseDto
{
    [XmlAttribute("SuccessesCount")]
    public int SuccessesCount
    {
        get => Successes.Count;
        set => _ = value;
    }

    [XmlAttribute("ErrorsCount")]
    public int ErrorsCount
    {
        get => Errors.Count;
        set => _ = value;
    }

    [XmlArray("Successes"), XmlArrayItem("Record")]
    public HashSet<ResponseSuccesses> Successes { get; set; } = [];

    [XmlArray("Errors"), XmlArrayItem("Record")]
    public HashSet<ResponseError> Errors { get; set; } = [];

    public void AddSuccess(IEnumerable<Guid> uidList)
    {
        foreach (Guid uid in uidList.ToHashSet())
            Successes.Add(new(uid));
    }

    public void AddError(Guid uid, string msg)
    {
        Errors.Add(new(uid, msg));
    }

    public void AddError(IEnumerable<Guid> uidList, string msg)
    {
        foreach (Guid uid in uidList.ToHashSet())
            AddError(uid, msg);
    }
}