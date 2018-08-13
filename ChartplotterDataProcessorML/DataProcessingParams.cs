using SharpConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML
{
    public class DataProcessingParams
    {
        public double DepthFilterLow { get; set; }
        public double DepthFilterHigh { get; set; }
        public int GpsRoundPrecision { get; set; }
        public int GpsErrorThreshold { get; set; }
        public int RegularizerTimeLoggingThreshold { get; set; }
        public int LocGeneratorRuns { get; set; }
        public int LocGeneratorShoreLineAggression { get; set; }

        public DataProcessingParams()
        {
            DepthFilterLow = 0.4;
            DepthFilterHigh = 6.0;
            GpsRoundPrecision = 4;
            GpsErrorThreshold = 115;
            RegularizerTimeLoggingThreshold = 172800;
            LocGeneratorRuns = 3;
            LocGeneratorShoreLineAggression = 12;

            if (File.Exists("appconfig.cfg"))
            {
                try
                {
                    var config = Configuration.LoadFromFile("appconfig.cfg");
                    var section = config["DataProcessingParams"];

                    DepthFilterLow = section["DepthFilterLow"].DoubleValue;
                    DepthFilterHigh = section["DepthFilterHigh"].DoubleValue;
                    GpsRoundPrecision = section["GpsRoundPrecision"].IntValue;
                    GpsErrorThreshold = section["GpsErrorThreshold"].IntValue;
                    RegularizerTimeLoggingThreshold = section["RegularizerTimeLoggingThreshold"].IntValue;
                    LocGeneratorRuns = section["LocGeneratorRuns"].IntValue;
                    LocGeneratorShoreLineAggression = section["LocGeneratorShoreLineAggression"].IntValue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
