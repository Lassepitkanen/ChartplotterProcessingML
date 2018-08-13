using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartplotterDataProcessorML.Tasks;

namespace ChartplotterDataProcessorML.UI
{
    public class UIMain
    {
        private readonly Dictionary<string, ITask> _tasks;
        private readonly InputValidator _inputValidator;
        private ProcessingContext Context;

        public UIMain(Dictionary<string,ITask> task, InputValidator inputValidator, ProcessingContext processingContext)
        {
            _tasks = task;
            _inputValidator = inputValidator;
            Context = processingContext;
        }
        
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("What do you want to do?");
                for (int i = 0; i < _tasks.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, _tasks.ElementAt(i).Key);
                }

                var number = _inputValidator.Validate(_tasks.Count);
                Console.Clear();
                RunTask(number);
                Console.WriteLine();
            }
        }

        public void RunTask(int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    _tasks[TaskName.CreateRepository].Run(Context);
                    break;
                case 2:
                    _tasks[TaskName.WebCrawler].Run(Context);
                    break;
                case 3:
                    if (Context.Rows.Count < 1)
                    {
                        _tasks[TaskName.LoadCsv].Run(Context);
                    }
                    _tasks[TaskName.ImageRecognition].Run(Context);
                    break;
                case 4:
                    if (Context.Rows.Count < 1)
                    {
                        _tasks[TaskName.LoadCsv].Run(Context);
                    }
                    _tasks[TaskName.DataProcessing].Run(Context);
                    break;
                case 5:
                    if (Context.Rows.Count < 1)
                    {
                        _tasks[TaskName.LoadCsv].Run(Context);
                    }
                    _tasks[TaskName.CreateCsv].Run(Context);
                    break;
                case 6:
                    if (Context.Rows.Count < 1)
                    {
                        _tasks[TaskName.LoadCsv].Run(Context);
                    }
                    _tasks[TaskName.CreateJson].Run(Context);
                    break;
                case 7:
                    _tasks[TaskName.LoadCsv].Run(Context);
                    break;
                case 8:
                    _tasks[TaskName.GetApi].Run(Context);
                    break;
                case 9:
                    _tasks[TaskName.Exit].Run(Context);
                    break;
            }
        }
    }
}
