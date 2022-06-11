using Models.WeatherConditionDetail;
using System;
using System.Collections.Generic;

namespace Models
{
    public class WeatherConditionModel
    {
        public CoordModel Coord { get; set; }
        public List<WeatherModel> Weather { get; set; }
        public MainModel Main { get; set; }
        public WindModel Wind { get; set; }
        public DateTime Time { get; set; }
        public int Timezone { get; set; }
        public string Visibility { get; set; }
        public string DewPoint { get; set; }
    }
}
