using Newtonsoft.Json;

namespace BeerQuest.Models
{
    [JsonObject]
    public class BusinessRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("tag")]
        public string Tag { get; set; }
        [JsonProperty("offset")]
        public int? Offset { get; set; }
        [JsonProperty("Limit")]
        public int? Limit { get; set; }
    }
}