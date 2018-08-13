using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace ChartplotterDataProcessorML.DataProcessing
{
    public class ShorelineFiller : IProcessData
    {
        private readonly DataProcessingParams _dataProcessingParams;

        public ShorelineFiller(DataProcessingParams dataProcessingParams)
        {
            _dataProcessingParams = dataProcessingParams;
        }

        public void Process(ProcessingContext context)
        {
            Console.WriteLine("Generating complete shoreline...");
            context.LocationSettings.Shoreline1 = GenerateCompleteShoreLine(context.LocationSettings.Shoreline1);
            context.LocationSettings.Shoreline2 = GenerateCompleteShoreLine(context.LocationSettings.Shoreline2);
        }

        private List<Shoreline> GenerateCompleteShoreLine(List<Shoreline> shoreline)
        {
            var shorelineLocations = new List<Shoreline>();
            for (int index = 0; index < shoreline.Count - 1; index++)
            {
                var lat = shoreline[index].GpsLat;
                var lng = shoreline[index].GpsLng;
                var latNext = shoreline[index + 1].GpsLat;
                var lngNext = shoreline[index + 1].GpsLng;

                var distance = Convert.ToInt32(CalculateDistance(lat, lng, latNext, lngNext) / 2);
                
                var stepLat = (latNext - lat) / distance;
                var stepLng = (lngNext - lng) / distance;

                shorelineLocations.Add(new Shoreline { GpsLat = lat, GpsLng = lng });
                for (int i = 0; i < distance; i++)
                {
                    lat = lat + stepLat;
                    lng = lng + stepLng;
                    shorelineLocations.Add(new Shoreline { GpsLat = lat, GpsLng = lng });
                }
            }
            return shorelineLocations;
        }

        private double CalculateDistance(decimal lat, decimal lng, decimal latNext, decimal lngnext)
        {
            var point1 = new GeoCoordinate(Convert.ToDouble(lat), Convert.ToDouble(lng));
            var point2 = new GeoCoordinate(Convert.ToDouble(latNext), Convert.ToDouble(lngnext));
            return point1.GetDistanceTo(point2);
        }
    }
}
