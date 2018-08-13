using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.Tests
{
    public class TestData
    {
        public ProcessingContext GetData()
        {
            List<Data> data = new List<Data>
            {
                new Data {UnixTime =  1509090005, GpsLat = 68.190879M, GpsLng = 23.148565M, WaterDepth = 4.1, HeatMap = 1},
                new Data {UnixTime =  1509090006, GpsLat = 68.190877M, GpsLng = 23.148565M, WaterDepth = 4.5, HeatMap = 1},
                new Data {UnixTime =  1509090007, GpsLat = 68.190958M, GpsLng = 23.148565M, WaterDepth = 4.8, HeatMap = 0},
                new Data {UnixTime =  1509090008, GpsLat = 68.190801M, GpsLng = 23.148524M, WaterDepth = 8.0, HeatMap = 0},
                new Data {UnixTime =  1509090009, GpsLat = 68.190878M, GpsLng = 23.148510M, WaterDepth = 8.0, HeatMap = 0},
                new Data {UnixTime =  1509090010, GpsLat = 68.190868M, GpsLng = 23.148606M, WaterDepth = 8.0, HeatMap = 0},
                new Data {UnixTime =  1509090011, GpsLat = 68.190834M, GpsLng = 23.148560M, WaterDepth = 1.2, HeatMap = 0},
                new Data {UnixTime =  1509090012, GpsLat = 68.190918M, GpsLng = 23.148545M, WaterDepth = 3.2, HeatMap = 0},
                new Data {UnixTime =  1509090013, GpsLat = 68.190835M, GpsLng = 23.148542M, WaterDepth = 6.1, HeatMap = 0},
                new Data {UnixTime =  1509090014, GpsLat = 68.190838M, GpsLng = 23.148598M, WaterDepth = 2.5, HeatMap = 0},
                new Data {UnixTime =  1509090015, GpsLat = 68.190834M, GpsLng = 23.148598M, WaterDepth = 8.1, HeatMap = 0},
                new Data {UnixTime =  1509090016, GpsLat = 68.190888M, GpsLng = 23.148598M, WaterDepth = 4.7, HeatMap = 0},
                new Data {UnixTime =  1509090017, GpsLat = 68.190918M, GpsLng = 23.148508M, WaterDepth = 2.5, HeatMap = 0},
                new Data {UnixTime =  1509090018, GpsLat = 68.190888M, GpsLng = 23.148506M, WaterDepth = 7.0, HeatMap = 0},
                new Data {UnixTime =  1509090019, GpsLat = 68.190878M, GpsLng = 23.148518M, WaterDepth = 6.6, HeatMap = 0},
                new Data {UnixTime =  1509090020, GpsLat = 68.190814M, GpsLng = 23.148526M, WaterDepth = 4.5, HeatMap = 0},
                new Data {UnixTime =  1509090021, GpsLat = 68.190877M, GpsLng = 23.148536M, WaterDepth = 4.5, HeatMap = 0},
                new Data {UnixTime =  1509090022, GpsLat = 68.190856M, GpsLng = 23.148499M, WaterDepth = 2.3, HeatMap = 0},
            };

            var settings = new LocationSettings
            {
                DefaultWaterLevel = 11
            };

            var waterLevels = new List<MeasurementPoint>
            {
                new MeasurementPoint { UnixTime = 1509090005, WaterLevel = 11.6 },
                new MeasurementPoint { UnixTime = 1509090012, WaterLevel = 9.0 },
                new MeasurementPoint { UnixTime = 1509090014, WaterLevel = 12 },
                new MeasurementPoint { UnixTime = 1509090016, WaterLevel = 9.0 },
                new MeasurementPoint { UnixTime = 1509090021, WaterLevel = 9.0 }
            };
            settings.MeasurementPoint = waterLevels;

            var testData = new ProcessingContext()
            {
                Rows = data,
                LocationSettings = settings,
            };
            return testData;
        }
    }
}
