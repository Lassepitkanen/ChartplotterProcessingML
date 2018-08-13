using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDataService.Models
{
    public class LocationSettings
    {
        public List<MeasurementPoint> MeasurementPoint { get; set; }
        public double DefaultWaterLevel { get; set; }
        public List<Shoreline> Shoreline1 { get; set; }
        public List<Shoreline> Shoreline2 { get; set; }
    }

    public class MeasurementPoint
    {
        public long UnixTime { get; set; }
        public double WaterLevel { get; set; }
    }

    public class Shoreline
    {
        public decimal GpsLat { get; set; }
        public decimal GpsLng { get; set; }
    }
}
