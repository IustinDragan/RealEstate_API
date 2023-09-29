using System.Text.Json;
using System.Text.Json.Serialization;
using RealEstate.DataAccess;

namespace RealEstate.Application.Helper;

public class AdressConverter : JsonConverter<Adress>
{
    public override Adress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Adress value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("street", value.Street);
        writer.WriteNumber("streetNumber", value.StreetNumber);
        writer.WriteString("country", value.Country);
        writer.WriteString("city", value.City);

        writer.WriteEndObject();
    }
}