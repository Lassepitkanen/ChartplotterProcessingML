using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChartplotterDataProcessorML
{
    public class Data
    {
        //JSON serializer ignores internal getters
        public double UnixTime { internal get; set; }
        public double WaterDepth { get; set; }
        public int HeatMap { internal get; set; }
        public decimal GpsLat { get; set; }
        public decimal GpsLng { get; set; }
        public double GpsAlt { internal get; set; }
        public double GpsSpeed { internal get; set; }
        public double GpsHeading { internal get; set; }
        public double GpsLatError { internal get; set; }
        public double GpsLngError { internal get; set; }
        public double GpsAltError { internal get; set; }
        public double AirTemp { internal get; set; }
        public double WaterTemp { internal get; set; }

        internal static Data ParseString(string line)
        {
            var columns = line.Split(',');
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i] == "NaN" || columns[i] == "")
                {
                    columns[i] = "0";
                }
            }

            return new Data
            {
                UnixTime = double.Parse(columns[0]),
                WaterDepth = double.Parse(columns[1]),
                HeatMap = int.Parse(columns[2]),
                GpsLat = decimal.Parse(columns[3]),
                GpsLng = decimal.Parse(columns[4]),
                GpsAlt = double.Parse(columns[5]),
                GpsSpeed = double.Parse(columns[6]),
                GpsHeading = double.Parse(columns[7]),
                GpsLatError = double.Parse(columns[8]),
                GpsLngError = double.Parse(columns[9]),
                GpsAltError = double.Parse(columns[10]),
                AirTemp = double.Parse(columns[11]),
                WaterTemp = double.Parse(columns[12])
            };
        }
    }
}
