using InMemoryDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDataService.Services
{
    public static class InMemoryLocationSettings
    {
        public  static LocationSettings CreateInMemoryLocationSettings()
        {
            var locationSettings = new LocationSettings
            {
                DefaultWaterLevel = 78,
                MeasurementPoint = new List<MeasurementPoint>() { new MeasurementPoint { UnixTime = 1533569333, WaterLevel = 77.8 } },
                Shoreline1 = new List<Shoreline>() {
                    new Shoreline() { GpsLat = 66.320642M, GpsLng = 23.650434M },
                    new Shoreline() { GpsLat = 66.315315M, GpsLng = 23.647945M },
                    new Shoreline() { GpsLat = 66.310871M, GpsLng = 23.647559M },
                    new Shoreline() { GpsLat = 66.307386M, GpsLng = 23.646051M }
                },
                Shoreline2 = new List<Shoreline>() {
                    new Shoreline() { GpsLat = 66.320395M, GpsLng = 23.661847M },
                    new Shoreline() { GpsLat = 66.312765M, GpsLng = 23.661418M },
                    new Shoreline() { GpsLat = 66.308213M, GpsLng = 23.660156M },
                    new Shoreline() { GpsLat = 66.320642M, GpsLng = 23.650434M }
                },
            };
            return locationSettings;
        }
    }
}