using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.Tasks
{
    public class ExitTask : ITask
    {
        public string Description => TaskName.Exit;

        public void Run(ProcessingContext context)
        {
            Environment.Exit(0);
        }
    }
}
