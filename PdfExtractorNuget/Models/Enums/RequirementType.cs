using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace PdfExtractor.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RequirementType { VALID, NORMAL, INVALID }
}
