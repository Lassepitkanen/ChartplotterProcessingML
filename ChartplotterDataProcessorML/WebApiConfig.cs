using SharpConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML
{
    public class WebApiConfig
    {
        public string LocationDataUrl { get; }
        public string LocationSettingsUrl { get; }

        public WebApiConfig()
        {
            LocationDataUrl = "http://localhost:8080/api/locationdata";
            LocationSettingsUrl = "http://localhost:8080/api/locationsettings";

            if (File.Exists("appconfig.cfg"))
            {
                try
                {
                    var config = Configuration.LoadFromFile("appconfig.cfg");
                    var section = config["DataServiceWebAPI"];

                    LocationDataUrl = section["LocationDataUrl"].StringValue;
                    LocationSettingsUrl = section["LocationSettingsUrl"].StringValue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
