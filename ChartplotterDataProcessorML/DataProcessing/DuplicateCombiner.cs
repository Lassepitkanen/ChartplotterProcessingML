using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.DataProcessing
{
    public class DuplicateCombiner : IProcessData
    {
        public void Process(ProcessingContext context)
        {
            Console.WriteLine("Combining duplicates...");
            foreach (var row in context.Rows.ToList())
            {
                var query = context.Rows.Where(x => x.GpsLat == row.GpsLat && x.GpsLng == row.GpsLng);
                SetAverageWaterDepthInDuplicates(query, row);
                SumDuplicateHeatMaps(query, row);
                RemoveAllButOneDuplicate(query, context);
            }
        }

        private void SetAverageWaterDepthInDuplicates(IEnumerable<Data> query, Data row)
        {
            var averageWaterDepth = query.Select(x => x.WaterDepth).Average();
            row.WaterDepth = Math.Round(averageWaterDepth, 1);
        }

        private void SumDuplicateHeatMaps(IEnumerable<Data> query, Data row)
        {
            row.HeatMap = query.Select(x => x.HeatMap).Sum();
        }

        private void RemoveAllButOneDuplicate(IEnumerable<Data> query, ProcessingContext context)
        {
            foreach (var item in query.Skip(1).ToList())
            {
                context.Rows.Remove(item);
            }
        }
    }
}
