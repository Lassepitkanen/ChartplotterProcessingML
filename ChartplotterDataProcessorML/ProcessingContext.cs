using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML
{
    public class ProcessingContext
    {
        public List<Data> Rows = new List<Data>();
        public LocationSettings LocationSettings = new LocationSettings();
    }
}
