using Newtonsoft.Json;
using System;

namespace Models
{
    public class CityModel
    {
        [JsonProperty("name")]
        public string CityName { get; set; }
        public string CountryCode { get; set; }
    }
}
