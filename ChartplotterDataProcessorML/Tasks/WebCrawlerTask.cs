using ChartplotterDataProcessorML.WaterLevelWebCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.Tasks
{
    public class WebCrawlerTask : ITask
    {
        private WebCrawlerMain _webCrawlerMain;

        public string Description => TaskName.WebCrawler;

        public WebCrawlerTask(WebCrawlerMain webCrawlerMain)
        {
            _webCrawlerMain = webCrawlerMain;
        }

        public void Run(ProcessingContext context)
        {
            _webCrawlerMain.Run();

            Console.Clear();
            Console.WriteLine("Water level measurements saved to cfg.");
            Console.WriteLine("If this was the first time, remember to change DefaultWaterLevel.");
        }
    }
}
