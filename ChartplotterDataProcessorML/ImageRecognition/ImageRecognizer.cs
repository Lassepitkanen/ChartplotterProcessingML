using Accord.MachineLearning.VectorMachines;
using Accord.Statistics.Kernels;
using ChartplotterDataProcessorML.FileIO;
using ChartplotterDataProcessorML.ImageRecognition.MachineLearning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition
{
    public class ImageRecognizer
    {
        private readonly IFileRepository _fileRepository;
        private readonly ImageLoader _imageLoader;
        private readonly InputCreator _inputCreator;
        private readonly PredictionHandler _predictionHandler;

        private string[] _targetImgPaths;
        private Bitmap[][] _bitmaps;

        public ImageRecognizer(IFileRepository fileRepository, ImageLoader imageLoader, InputCreator inputCreator, PredictionHandler predictionHandler)
        {
            _fileRepository = fileRepository;
            _imageLoader = imageLoader;
            _inputCreator = inputCreator;
            _predictionHandler = predictionHandler;
        }

        public List<ImageData> Run(MultilabelSupportVectorMachine<Linear> machine1, MultilabelSupportVectorMachine<Linear> machine2)
        {
            LoadTargetImages();
            return GetPredictions(machine1, machine2);
        }

        private void LoadTargetImages()
        {
            _targetImgPaths = _fileRepository.GetAllFiles(_fileRepository.ImageDir, "jpg");
            _bitmaps = _imageLoader.LoadTwoDigitImages(_targetImgPaths);
        }
         
        private List<ImageData> GetPredictions(MultilabelSupportVectorMachine<Linear> machine1, MultilabelSupportVectorMachine<Linear> machine2 )
        {
            var firstDigitInputs = _inputCreator.ConvertToArrays(_bitmaps[0]);
            var secondDigitInputs = _inputCreator.ConvertToArrays(_bitmaps[1]);
            var firstDigitPredict = machine1.Decide(firstDigitInputs);
            var secondDigitPredict = machine2.Decide(secondDigitInputs);

            return _predictionHandler.HandlePredictions(firstDigitPredict, secondDigitPredict, _targetImgPaths);
        }
    }
}
