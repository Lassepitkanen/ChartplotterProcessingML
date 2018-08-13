using Accord.MachineLearning.VectorMachines;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition.MachineLearning
{
    public class Machine
    {
        private readonly InputCreator _inputCreator;
        private readonly OutputCreator _outputCreator;
        private readonly Learning _learning;
        private readonly ImageLoader _imageLoader;

        public Machine(InputCreator inputCreator, OutputCreator outputCreator, Learning learning, ImageLoader imageLoader)
        {
            _inputCreator = inputCreator;
            _outputCreator = outputCreator;
            _learning = learning;
            _imageLoader = imageLoader;
        }

        public MultilabelSupportVectorMachine<Linear> ConstructMachine(string learningImgDir, Rectangle digitRectangle)
        {
            var learningImgPaths = _imageLoader.LoadModelFolders(learningImgDir);
            var learningBitmaps = _imageLoader.LoadLearningImages(learningImgPaths, digitRectangle);
            return Teach(learningImgPaths, learningBitmaps);
        }

        private MultilabelSupportVectorMachine<Linear> Teach(IEnumerable<string[]> ImgPaths, Bitmap[] bitmaps)
        {
            double[][] inputs = _inputCreator.ConvertToArrays(bitmaps);
            int[] outputs = _outputCreator.GetOutputModels(ImgPaths);
            return _learning.Teach(inputs, outputs);
        }
    }
}

