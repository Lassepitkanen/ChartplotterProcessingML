using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.WaterLevelWebCrawler
{
    public class WebCrawlerMain
    {
        private readonly HttpDownloader _httpDownloader;
        private readonly IMeasurementPointUtility _measurementPointUtility;
        private readonly StringEditUtility _stringEditUtility;

        public WebCrawlerMain(HttpDownloader httpDownloader, IMeasurementPointUtility measurementPointUtility, StringEditUtility stringEditUtility)
        {
            _httpDownloader = new HttpDownloader();
            _measurementPointUtility = measurementPointUtility;
            _stringEditUtility = stringEditUtility;
        }

        public void Run()
        {
            Console.WriteLine("Loading water level measurements");
            var measurementPoints = _measurementPointUtility.LoadMeasurementPoints();
            foreach (var item in measurementPoints)
            {
                var html = _httpDownloader.Download(item.Url);
                var waterLevels = _stringEditUtility.GetWaterLevelData(html);
                _measurementPointUtility.SaveMeasurementPoints(item.Location, waterLevels);
            }
        }
    }
}
