using ChartplotterDataProcessorML.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.Tasks
{
    public class LoadCsvTask : ITask
    {
        private readonly ProcessingContextLoader _contextLoader;

        public string Description => TaskName.LoadCsv;

        public LoadCsvTask(ProcessingContextLoader contextLoader)
        {
            _contextLoader = contextLoader;
        }

        public void Run(ProcessingContext context)
        {
            _contextLoader.LoadContext(context);

            Console.Clear();
            Console.WriteLine("Csvs Loaded");
        }
    }
}
