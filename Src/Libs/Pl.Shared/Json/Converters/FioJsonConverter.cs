namespace Pl.Shared.Json.Converters;

public class FioJsonConverter : JsonConverter<Fio>
{
    public override Fio Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        if (!root.TryGetProperty("name", out JsonElement nameElement) ||
            !root.TryGetProperty("surname", out JsonElement surnameElement) ||
            !root.TryGetProperty("patronymic", out JsonElement patronymicElement))
            throw new JsonException("Missing required properties for Fio.");

        string name = nameElement.GetString() ?? throw new JsonException("Missing 'name' property");
        string surname = surnameElement.GetString() ?? throw new JsonException("Missing 'surname' property");
        string patronymic = patronymicElement.GetString() ?? throw new JsonException("Missing 'patronymic' property");

        return new(surname, name, patronymic);
    }

    public override void Write(Utf8JsonWriter writer, Fio value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", value.Name);
        writer.WriteString("surname", value.Surname);
        writer.WriteString("patronymic", value.Patronymic);
        writer.WriteEndObject();
    }
}