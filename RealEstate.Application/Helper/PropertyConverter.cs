using System.Text.Json;
using System.Text.Json.Serialization;
using RealEstate.DataAccess;

namespace RealEstate.Application.Helper;

public class PropertyConverter : JsonConverter<Property>
{
    public override Property? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Property value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("roomsNumber", value.RoomsNumber); //ToDo: de continuat
        //writer.WriteString("completeAddress", $"{value.Adress.Street} {value.Adress.StreetNumber}, {value.Adress.City}, {value.Adress.Country}");

        writer.WriteEndObject();
    }
}