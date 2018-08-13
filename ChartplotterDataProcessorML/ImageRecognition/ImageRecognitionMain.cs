using Accord.Math;
using ChartplotterDataProcessorML.FileIO;
using ChartplotterDataProcessorML.ImageRecognition.MachineLearning;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition
{
    public class ImageRecognitionMain
    {
        private readonly IFileRepository _fileRepository;
        private readonly Machine _ml;
        private readonly BitmapProcessingParams _bitmapProcessingParams;
        private readonly ImageRecognizer _imageRecognizer;
        private readonly ILogger _logger;

        public ImageRecognitionMain(IFileRepository fileRepository, BitmapProcessingParams bitmapProcessingParams, Machine machine, ImageRecognizer imageRecognizer, ILogger logger)
        {
            _fileRepository = fileRepository;
            _bitmapProcessingParams = bitmapProcessingParams;
            _ml = machine;
            _imageRecognizer = imageRecognizer;
            _logger = logger;
        }

        public void Run(ProcessingContext context)
        {
            Console.WriteLine("Loading learning images and constructing machines...");
            var machine1 = _ml.ConstructMachine(_fileRepository.FirstDigitLearningImagesDir, _bitmapProcessingParams.FirstDigitRectangle);
            var machine2 = _ml.ConstructMachine(_fileRepository.SecondDigitLearningImagesDir, _bitmapProcessingParams.SecondDigitRectangle);

            Console.WriteLine("Loading and recognizing target images");
            var imageData = _imageRecognizer.Run(machine1, machine2);

            _logger.Information("Images Recognized: {0}", imageData.Count);
            Console.WriteLine("Images recognized, matching to ProcessingContext");
            var unixTimes = new HashSet<double>(context.Rows.Select(x => x.UnixTime));
            foreach (var item in imageData)
            {
                if (unixTimes.Contains(item.UnixTime))
                {
                    double depth = item.FirstDigit + ((double)item.SecondDigit / 10);
                    context.Rows.First(x => x.UnixTime == item.UnixTime).WaterDepth = depth;
                }
            }
        }
    }
}
