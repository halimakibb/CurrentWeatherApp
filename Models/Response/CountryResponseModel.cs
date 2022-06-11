using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CountryResponseModel
    {
        [JsonProperty("error")]
        public bool IsError { get; set; }
        [JsonProperty("msg")]
        public string ErrorMessage { get; set; }
        public List<CountryModel> Data { get; set; }
    }
}
