namespace Pl.Mobile.Client.Source.Shared;

public record PluDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
}