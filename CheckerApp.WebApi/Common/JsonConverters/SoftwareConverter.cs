﻿using CheckerApp.Application.Softwares.Queries.GetSoftwaresList;
using CheckerApp.Domain.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CheckerApp.WebApi.Common.JsonConverters
{
    public class SoftwareConverter : JsonConverter<SoftwareDto>
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeof(SoftwareDto).IsAssignableFrom(typeToConvert);

        public override SoftwareDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                if (!jsonDocument.RootElement.TryGetProperty("SoftwareType", out var typeProperty))
                {
                    throw new JsonException();
                }

                var type = (SoftwareType)typeProperty.GetInt32();

                var jsonObject = jsonDocument.RootElement.GetRawText();

                return type switch
                {
                    SoftwareType.SCADA => JsonSerializer.Deserialize<ScadaDto>(jsonObject),
                    SoftwareType.Other => JsonSerializer.Deserialize<SoftwareDto>(jsonObject),
                    _ => throw new NotSupportedException()
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, SoftwareDto value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value);
        }
    }
}
