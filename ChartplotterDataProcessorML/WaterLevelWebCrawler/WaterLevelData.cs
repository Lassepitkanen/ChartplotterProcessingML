using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.WaterLevelWebCrawler
{
    public class WaterLevelData
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public double WaterLevel { get; set; }

        internal static WaterLevelData ParseString(string line)
        {
            var columns = line.Split(',');

            return new WaterLevelData
            {
                Year = int.Parse(columns[0]),
                Month = int.Parse(columns[1]) + 1, //January is 0, Feb 1 etc..
                Day = int.Parse(columns[2]),
                Hour = int.Parse(columns[3]),
                WaterLevel = double.Parse(columns[4])
            };
        }
    }
}
