using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.DataProcessing
{
    public class Filters : IProcessData
    {
        private readonly DataProcessingParams _dataProcessingParams;
        private readonly ILogger _logger;

        public Filters(DataProcessingParams dataProcessingParams, ILogger logger)
        {
            _dataProcessingParams = dataProcessingParams;
            _logger = logger;
        }

        public void Process(ProcessingContext context)
        {
            var config = _dataProcessingParams;

            var rowCount = context.Rows.Count;
            _logger.Information("Rows in ProcessingContext: {0}", rowCount);
            context.Rows.RemoveAll(x => x.WaterDepth <= config.DepthFilterLow || x.WaterDepth >= config.DepthFilterHigh);
            _logger.Information("(DephFilter)Rows filtered: {0}", rowCount - context.Rows.Count);

            rowCount = context.Rows.Count;
            context.Rows.RemoveAll(x => x.GpsLatError >= config.GpsErrorThreshold || x.GpsLngError >= config.GpsErrorThreshold);
            _logger.Information("(GpsError)Rows filtered: {0}", rowCount - context.Rows.Count);
        }
    }
}
