﻿using CheckerApp.Application.Hardwares.Queries;
using CheckerApp.Domain.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CheckerApp.WebApi.Common.JsonConverters
{
    public class HardwareConverter : JsonConverter<HardwareDto>
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeof(HardwareDto).IsAssignableFrom(typeToConvert);

        public override HardwareDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                if (!jsonDocument.RootElement.TryGetProperty("HardwareType", out var typeProperty))
                {
                    throw new JsonException();
                }
                var type = (HardwareType)typeProperty.GetInt32();

                var jsonObject = jsonDocument.RootElement.GetRawText();

                return type switch
                {
                    HardwareType.Cabinet => JsonSerializer.Deserialize<CabinetDto>(jsonObject),
                    HardwareType.FlowComputer => JsonSerializer.Deserialize<FlowComputerDto>(jsonObject),
                    HardwareType.Flowmeter => JsonSerializer.Deserialize<FlowmeterDto>(jsonObject),
                    HardwareType.Network => JsonSerializer.Deserialize<NetworkHardwareDto>(jsonObject),
                    HardwareType.PLC => JsonSerializer.Deserialize<PlcDto>(jsonObject),
                    HardwareType.Pressure => JsonSerializer.Deserialize<PressureDto>(jsonObject),
                    HardwareType.Temperature => JsonSerializer.Deserialize<TemperatureDto>(jsonObject),
                    HardwareType.Valve => JsonSerializer.Deserialize<ValveDto>(jsonObject),
                    HardwareType.ARM => JsonSerializer.Deserialize<ArmDto>(jsonObject),
                    HardwareType.APC => JsonSerializer.Deserialize<ApcDto>(jsonObject),
                    HardwareType.DiffPressure => JsonSerializer.Deserialize<DiffPressureDto>(jsonObject),
                    HardwareType.GasAnalyzer => JsonSerializer.Deserialize<GasAnalyzerDto>(jsonObject),
                    HardwareType.FireSensor => JsonSerializer.Deserialize<FireSensorDto>(jsonObject),
                    HardwareType.FireModule => JsonSerializer.Deserialize<FireModuleDto>(jsonObject),
                    HardwareType.InformPanel => JsonSerializer.Deserialize<InformPanelDto>(jsonObject),
                    _ => throw new NotSupportedException(),
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, HardwareDto value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value);
        }
    }
}
