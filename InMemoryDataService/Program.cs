using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;

namespace InMemoryDataService
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApp.Start(new StartOptions { Port = 8080 }, app =>
            {
                var config = new HttpConfiguration();
                config.Routes.MapHttpRoute("Default", "api/{controller}");
                config.Formatters.Remove(config.Formatters.XmlFormatter);

                app.UseWebApi(config);
            });
            Console.WriteLine("In-Memory Data Service");
            Console.Read();
        }
    }
}
