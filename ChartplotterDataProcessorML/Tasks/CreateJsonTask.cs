using ChartplotterDataProcessorML.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.Tasks
{
    public class CreateJsonTask : ITask
    {
        private readonly JsonUtility _jsonUtility;

        public string Description => TaskName.CreateJson;

        public CreateJsonTask(JsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
        }

        public void Run(ProcessingContext context)
        {
            _jsonUtility.CreateJson(context.Rows);

            Console.Clear();
            Console.WriteLine("JSON Created");
        }
    }
}
