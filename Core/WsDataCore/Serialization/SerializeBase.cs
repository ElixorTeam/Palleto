namespace WsDataCore.Serialization;

[Serializable]
public class SerializeBase : ISerializable
{
    #region Public and private fields, properties, constructor

    public SerializeBase() { }

    protected SerializeBase(SerializationInfo info, StreamingContext context) { }

    public SerializeBase(SerializeBase item) { }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        //
    }

    #endregion
}