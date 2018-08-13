using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.DataProcessing
{
    public interface IProcessData
    {
        void Process(ProcessingContext context);
    }

    public class ProcessData : IProcessData
    {
        private readonly IEnumerable<IProcessData> _subProcessors;

        public ProcessData(IEnumerable<IProcessData> subProcessors)
        {
            _subProcessors = subProcessors;
        }

        public void Process(ProcessingContext context)
        {
            foreach (var processor in _subProcessors)
            {
                processor.Process(context);
            }
        }
    }   
}
