using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition.MachineLearning
{
    public class Learning
    {
        public MultilabelSupportVectorMachine<Linear> Teach(double[][] inputs, int[] outputs)
        {
            var teacher = new MultilabelSupportVectorLearning<Linear>()
            {
                Learner = (p) => new LinearDualCoordinateDescent()
                {
                    Loss = Loss.L2
                }
            };
            return teacher.Learn(inputs, outputs);
        }
    }
}
