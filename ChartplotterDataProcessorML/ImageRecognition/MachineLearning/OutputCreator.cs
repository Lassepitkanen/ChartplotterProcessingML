using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition.MachineLearning
{
    public class OutputCreator
    {
        public int[] GetOutputModels(IEnumerable<string[]> paths)
        {
            var outputList = new List<int>();
            var count = 0; //models from 0 ->
            foreach (var path in paths)
            {
                foreach (var item in path)
                {
                    outputList.Add(count);
                }
                count = count + 1;
            }
            return outputList.ToArray();
        }
    }
}
