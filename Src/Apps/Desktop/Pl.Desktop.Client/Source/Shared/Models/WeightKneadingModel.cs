namespace Pl.Desktop.Client.Source.Shared.Models;

public class WeightKneadingModel
{
    public decimal NetWeight { get; set; }
    public DateTime ProductDate { get; set; } = DateTime.Now;
    public ushort KneadingCount { get; set; } = 1;
}