using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace BlazorMsgPack.Shared
{
    [MessagePackObject]
    public class WeatherForecast
    {
        [Key(0)]
        public DateTime Date { get; set; }
        [Key(1)]
        public int TemperatureC { get; set; }
        [Key(2)]
        public string Summary { get; set; }
        [Key(3)]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
