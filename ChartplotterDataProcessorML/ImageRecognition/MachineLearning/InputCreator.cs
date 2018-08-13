using Accord.Imaging.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition.MachineLearning
{
    public class InputCreator
    {
        public double[][] ConvertToArrays(Bitmap[] bitmaps)
        {
            double[][] inputs = new double[bitmaps.Length][];
            var imageToArray = new ImageToArray(min: 0, max: 1);

            for (int i = 0; i < inputs.Length; i++)
            {
                imageToArray.Convert(bitmaps[i], out double[] imgArray);
                inputs[i] = imgArray;
                bitmaps[i].Dispose();
            }
            return inputs;
        }
    }
}
