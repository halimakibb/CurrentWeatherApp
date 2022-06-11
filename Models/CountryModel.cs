using Newtonsoft.Json;
using System;

namespace Models
{
    public class CountryModel
    {
        [JsonProperty("iso2")]
        public string CountryCode { get; set; }
        [JsonProperty("name")]
        public string CountryName { get; set; }
    }
}
