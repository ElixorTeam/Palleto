using TscZebra.Plugin.Abstractions.Enums;

namespace Ws.DeviceControl.Models.Features.Devices.Printers.Commands;

public sealed record PrinterUpdateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpV4AddressJsonConverter))]
    public IPAddress Ip { get; set; } = IPAddress.None;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<PrinterTypes>))]
    public PrinterTypes Type { get; set; } = PrinterTypes.Tsc;
}

public class PrinterUpdateValidator : AbstractValidator<PrinterUpdateDto>
{
    public PrinterUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .MaximumLength(16).NotEmpty().Matches("^[A-Z0-9-]*$")
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Ip).NotEmpty().NotEqual(IPAddress.None);
        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
    }
}