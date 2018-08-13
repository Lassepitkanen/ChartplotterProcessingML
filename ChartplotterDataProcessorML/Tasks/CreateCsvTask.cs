using ChartplotterDataProcessorML.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChartplotterDataProcessorML.Tasks
{
    public class CreateCsvTask : ITask
    {
        private readonly CsvUtility _csvUtility;

        public string Description => TaskName.CreateCsv;

        public CreateCsvTask(CsvUtility csvUtility)
        {
            _csvUtility = csvUtility;
        }

        public void Run(ProcessingContext context)
        {
            _csvUtility.CreateCsv(context.Rows);

            Console.Clear();
            Console.WriteLine("Csv Created");
        }
    }
}
