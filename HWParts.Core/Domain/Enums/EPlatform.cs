using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HWParts.Core.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EPlatform
    {
        NewEgg,
        Kabum,
        Terabyte,
        Pichau
    }
}
