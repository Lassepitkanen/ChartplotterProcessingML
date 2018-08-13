using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.DataProcessing
{
    public class DepthRegularizer : IProcessData
    {
        private readonly DataProcessingParams _dataProcessingParams;
        private readonly ILogger _logger;

        public DepthRegularizer(DataProcessingParams dataProcessingParams, ILogger logger)
        {
            _dataProcessingParams = dataProcessingParams;
            _logger = logger;
        }

        public void Process(ProcessingContext context)
        {
            var config = _dataProcessingParams;

            foreach (var row in context.Rows.Where(x => x.WaterDepth > 0))
            {
                var closestMeasurementTime = context.LocationSettings.MeasurementPoint.Aggregate((x, y) => Math.Abs(x.UnixTime - row.UnixTime) < Math.Abs(y.UnixTime - row.UnixTime) ? x : y);
                var variance = Math.Round(context.LocationSettings.DefaultWaterLevel - closestMeasurementTime.WaterLevel, 1);
                row.WaterDepth += variance;

                LogTimeDifferenceToClosestWaterMeasurement(row, closestMeasurementTime);
            }
        }

        private void LogTimeDifferenceToClosestWaterMeasurement(Data row, MeasurementPoint closestMeasurementTime)
        {
            var timeDifference = Math.Abs(closestMeasurementTime.UnixTime - row.UnixTime);
            if (timeDifference > _dataProcessingParams.RegularizerTimeLoggingThreshold)
            {
                _logger.Information("More than {0} seconds for closest measurement:" + row.UnixTime, _dataProcessingParams.RegularizerTimeLoggingThreshold);
            }
        }
    }
}
