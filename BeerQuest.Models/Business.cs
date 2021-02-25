using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeerQuest.Models
{
    [JsonObject]
    public class Businesses
    {
        [JsonProperty("offset")]
        public int? Offset { get; set; }
        [JsonProperty("Limit")]
        public int? Limit { get; set; }
        [JsonProperty("businessCollection")]
        public IReadOnlyCollection<Business> BusinessCollection { get; set; }
    }

    [JsonObject]
    public class Business
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }
        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
        [JsonProperty("lat")]
        public float Lat { get; set; }
        [JsonProperty("lng")]
        public float Lng { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("twitter")]
        public string Twitter { get; set; }
        [JsonProperty("starsBeer")]
        public decimal StarsBeer { get; set; }
        [JsonProperty("starsAtmosphere")]
        public decimal StarsAtmosphere { get; set; }
        [JsonProperty("starsAmenities")]
        public decimal StarsAmenities { get; set; }
        [JsonProperty("starsValue")]
        public decimal StarsValue { get; set; }
        [JsonProperty("tags")]
        public string Tags { get; set; }
        [JsonProperty("totalRows")]
        public int TotalRows { get; set; }
    }
}
