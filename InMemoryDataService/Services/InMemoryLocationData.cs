using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InMemoryDataService.Models;

namespace InMemoryDataService.Services
{
    public static class InMemoryLocationData
    {
        public  static List<Location> CreateInMemoryLocationData()
        {
            var rnd = new Random();

            var locationData = new List<Location>();
            for (int i = 0; i < 2000; i++)
            {
                var waterDepth = Math.Round(rnd.NextDouble() * 6, 1);

                var heatMap = 0;
                if (i == 500 || i == 1000 || i == 1500)
                {
                    heatMap = 1;
                }

                var gpsLat = Convert.ToDecimal(
                    Math.Round(
                        rnd.NextDouble() * (66.320566 - 66.309327) + 66.309327, 6));
                var gpsLng = Convert.ToDecimal(
                    Math.Round(
                        rnd.NextDouble() * (23.659237 - 23.651168) + 23.651168, 6));

                locationData.Add(new Location
                {
                    UnixTime = 1533569333 + i,
                    WaterDepth = waterDepth,
                    HeatMap = heatMap,
                    GpsLat = gpsLat,
                    GpsLng = gpsLng,
                    GpsAlt = 100,
                    GpsSpeed = 6.2,
                    GpsHeading = 90,
                    GpsLatError = 11,
                    GpsAltError = 11,
                    AirTemp = 20,
                    WaterTemp = 11
                });
            }
            return locationData;
        }
    }
}
