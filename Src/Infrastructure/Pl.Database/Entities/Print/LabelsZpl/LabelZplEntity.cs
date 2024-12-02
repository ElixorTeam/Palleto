namespace Pl.Database.Entities.Print.LabelsZpl;

public sealed class LabelZplEntity
{
    public Guid Id { get; set; }
    public short Width { get; set; }
    public short Height { get; set; }
    public short Rotate { get; set; }
    public string Zpl { get; set; } = string.Empty;
}