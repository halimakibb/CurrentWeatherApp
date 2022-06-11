using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.WeatherConditionDetail
{
    public class MainModel
    {
        public string Temp { get; set; }
        public string TempC { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
    }
}
