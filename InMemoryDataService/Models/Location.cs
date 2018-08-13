using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDataService.Models
{
    public class Location
    {
        public double UnixTime { get; set; }
        public double WaterDepth { get; set; }
        public int HeatMap { get; set; }
        public decimal GpsLat { get; set; }
        public decimal GpsLng { get; set; }
        public double GpsAlt { get; set; }
        public double GpsSpeed { get; set; }
        public double GpsHeading { get; set; }
        public double GpsLatError { get; set; }
        public double GpsLngError { get; set; }
        public double GpsAltError { get; set; }
        public double AirTemp { get; set; }
        public double WaterTemp { get; set; }
    }
}
