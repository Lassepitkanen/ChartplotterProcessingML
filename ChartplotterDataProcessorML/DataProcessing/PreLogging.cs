using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.DataProcessing
{
    public class PreLogging : IProcessData
    {
        private readonly DataProcessingParams _dataProcessingParams;
        private readonly ILogger _logger;

        public PreLogging(DataProcessingParams dataProcessingParams, ILogger logger)
        {
            _dataProcessingParams = dataProcessingParams;
            _logger = logger;
        }

        public void Process(ProcessingContext context)
        {
            LogGpsErrors(context);
            LogMaxSpeed(context);
            LogHeatMaps(context);
        }

        private void LogGpsErrors(ProcessingContext context)
        {
            var averageGpsLatError = context.Rows.Average(x => x.GpsLatError);
            var averageGpsLngError = context.Rows.Average(x => x.GpsLngError);
            _logger.Information("Average GpsLatError: {0}", averageGpsLatError);
            _logger.Information("Average GpsLngError: {0}", averageGpsLngError);

            var maxGpsLatError = context.Rows.Max(x => x.GpsLatError);
            var maxGpsLngError = context.Rows.Max(x => x.GpsLngError);
            _logger.Information("Max GpsLatError: {0}", maxGpsLatError);
            _logger.Information("Max GpsLngError: {0}", maxGpsLngError);

            var minGpsLatError = context.Rows.Min(x => x.GpsLatError);
            var minGpsLngError = context.Rows.Min(x => x.GpsLngError);
            _logger.Information("Min GpsLatError: {0}", minGpsLatError);
            _logger.Information("Min GpsLngError: {0}", minGpsLngError);
        }

        private void LogMaxSpeed(ProcessingContext context)
        {
            var maxGpsSpeed = context.Rows.Max(x => x.GpsSpeed);
            _logger.Information("Max GpsSpeed: {0}", maxGpsSpeed);
        }

        private void LogHeatMaps(ProcessingContext context)
        {
            var heatMaps = context.Rows.Where(x => x.HeatMap > 0);
            foreach (var item in heatMaps)
            {
                _logger.Information("HeatMap at UnixTime: {0}, WaterDepth: {1}", item.UnixTime, item.WaterDepth);
            }
        }
    }
}
