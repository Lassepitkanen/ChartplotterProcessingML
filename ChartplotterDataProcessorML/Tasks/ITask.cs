using ChartplotterDataProcessorML.ImageRecognition;
using ChartplotterDataProcessorML.WaterLevelWebCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.Tasks
{
    public interface ITask
    {
        string Description { get; }
        void Run(ProcessingContext context);
    }
}
