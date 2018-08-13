using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML
{
    public class TimeConverter
    {
        public long ConvertToUnixTime(DateTime dateTime)
        {
            var sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return (long)(dateTime - sTime).TotalSeconds;
        }

        public long ConvertToUnixTime(int year, int month, int day, int hour, int minute, int second)
        {
            var dateTime = new DateTime(year, month, day, hour, minute, second);
            var sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

            return (long)(dateTime - sTime).TotalSeconds;
        }
    }
}
