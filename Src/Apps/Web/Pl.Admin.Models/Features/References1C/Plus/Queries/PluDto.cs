// ReSharper disable ClassNeverInstantiated.Global
namespace Pl.Admin.Models.Features.References1C.Plus.Queries;

public sealed record PluDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("number")]
    public required ushort Number { get; init; }

    [JsonPropertyName("type")]
    public required bool IsWeight { get; init; }

    [JsonPropertyName("weight")]
    public required decimal Weight { get; init; }

    [JsonPropertyName("brand")]
    public required ProxyDto Brand { get; init; }

    [JsonPropertyName("shelfLifeDays")]
    public required ushort ShelfLifeDays { get; init; }

    [JsonPropertyName("template")]
    public required ProxyDto? Template { get; init; }

    [JsonPropertyName("storageMethods")]
    public required string StorageMethod { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("fullName")]
    public required string FullName { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("ean13")]
    public required string Ean13 { get; init; }

    [JsonPropertyName("gtin")]
    public required string Gtin { get; init; }

    [JsonPropertyName("clip")]
    public required ProxyDto? Clip { get; init; }

    [JsonPropertyName("bundle")]
    public required ProxyDto? Bundle { get; init; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; init; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; init; }
}

public class PluViewValidator : AbstractValidator<PluDto>
{
    public PluViewValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Template)
            .NotNull().WithName(wsDataLocalizer["ColTemplate"]);

        RuleFor(item => item.StorageMethod)
            .Must(value => value is "Замороженное" or "Охлаждённое")
            .WithMessage("Способ хранения - должен быть ['Замороженное', 'Охлаждённое']");
    }
}