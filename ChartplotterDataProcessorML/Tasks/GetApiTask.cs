using ChartplotterDataProcessorML.WaterLevelWebCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChartplotterDataProcessorML.Tasks
{
    public class GetApiTask : ITask
    {
        private readonly WebApiDataLoader _webApiDataLoader;

        public string Description => TaskName.GetApi;

        public GetApiTask(WebApiDataLoader webApiDataLoader)
        {
            _webApiDataLoader = webApiDataLoader;         
        }

        public void Run(ProcessingContext context)
        {
            _webApiDataLoader.Run(context);
            Console.Clear();
            Console.WriteLine("Data Loaded");
        }
    }
}
