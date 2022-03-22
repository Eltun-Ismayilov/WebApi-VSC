using System.Text.Json.Serialization;

namespace WebApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum  RpgClass
    {
        Knight,
        Mage,
        Cleric
    }
}