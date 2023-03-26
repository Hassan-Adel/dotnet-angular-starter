using System.Text.Json.Serialization;

namespace dotnet_angular_starter.Models;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TodoType
{
    Private = 1,
    Shared = 2
}
