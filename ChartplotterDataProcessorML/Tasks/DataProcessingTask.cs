using ChartplotterDataProcessorML.DataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.Tasks
{
    public class DataProcessingTask : ITask
    {
        private readonly IProcessData _processData;

        public string Description => TaskName.DataProcessing;

        public DataProcessingTask(IProcessData processData)
        {
            _processData = processData;
        }
        
        public void Run(ProcessingContext context)
        {
            _processData.Process(context);

            Console.Clear();
            Console.WriteLine("Data processing complete");
        }
    }
}
