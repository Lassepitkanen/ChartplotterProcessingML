using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            if (Debugger.IsAttached)
            {
                RunInMemoryDataService();
            }

            var bootstrapper = new Bootstrapper();
            bootstrapper.UIMain.Run();
        }

        public static void RunInMemoryDataService()
        {
            var dir = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Path.Combine(dir, @"..\..\..\\InMemoryDataService\bin\Debug\InMemoryDataService.exe")); 
            Process.Start(path);
        }
    }
}

