using ChartplotterDataProcessorML.WaterLevelWebCrawler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML
{
    public class WebApiDataLoader
    {
        private readonly HttpDownloader _httpDownloader;
        private readonly WebApiConfig _webApiConfig;

        public WebApiDataLoader(HttpDownloader httpDownloader, WebApiConfig webApiConfig)
        {
            _httpDownloader = httpDownloader;
            _webApiConfig = webApiConfig;
        }

        public ProcessingContext Run(ProcessingContext context)
        {
            Console.WriteLine("Loading Data...");
            var locationData = _httpDownloader.Download(_webApiConfig.LocationDataUrl);
            context.Rows = JsonConvert.DeserializeObject<List<Data>>(locationData);

            var locationSettings = _httpDownloader.Download(_webApiConfig.LocationSettingsUrl);
            context.LocationSettings = JsonConvert.DeserializeObject<LocationSettings>(locationSettings);
            return context;
        }
    }
}
