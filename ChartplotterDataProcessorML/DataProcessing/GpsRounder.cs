using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.DataProcessing
{
    public class GpsRounder : IProcessData
    {
        private readonly DataProcessingParams _dataProcessingParams;

        public GpsRounder(DataProcessingParams dataProcessingParams)
        {
            _dataProcessingParams = dataProcessingParams;
        }

        public void Process(ProcessingContext context)
        {
            var config = _dataProcessingParams;

            foreach (var item in context.Rows)
            {
                var gpsLat = (item.GpsLat * 2);
                gpsLat = Math.Round(gpsLat, config.GpsRoundPrecision, MidpointRounding.AwayFromZero);
                item.GpsLat = (gpsLat / 2);

                item.GpsLng = Math.Round(item.GpsLng, config.GpsRoundPrecision, MidpointRounding.AwayFromZero);
            }
        }
    }
}
