using ChartplotterDataProcessorML.FileIO;
using ChartplotterDataProcessorML.ImageRecognition.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition
{
    public class PredictionHandler
    {
        private readonly IFileRepository _fileRepository;
        private readonly ILogger _logger;
        private readonly IModelMatcher _firstDigitModelMatcher;
        private readonly IModelMatcher _secondDigitModelMatcher;

        public PredictionHandler(IFileRepository fileRepository, IModelMatcher firstDigitModelMatcher, IModelMatcher secondDigitModelMatcher, ILogger logger)
        {
            _firstDigitModelMatcher = firstDigitModelMatcher;
            _secondDigitModelMatcher = secondDigitModelMatcher;
            _fileRepository = fileRepository;
            _logger = logger;
        }

        public List<ImageData> HandlePredictions(bool[][] firstDigitPredict, bool[][] secondDigitPredict, string[] imgPaths)
        {
            int firstCopyCount = 0;
            int secondCopyCount =  0;

            var imageData = new List<ImageData>();
            for (int i = 0; i < firstDigitPredict.Length; i++)
            {
                var firstDigitValue = _firstDigitModelMatcher.MatchModel(firstDigitPredict[i]);
                var secondDigitValue = _secondDigitModelMatcher.MatchModel(secondDigitPredict[i]);

                if (firstDigitValue == -1)
                {
                    CopyNotMatchedImage(imgPaths[i], _fileRepository.FirstDigitNotMatchedImagesDir);
                    firstCopyCount++;
                }
                else if (secondDigitValue == -1)
                {
                    CopyNotMatchedImage(imgPaths[i], _fileRepository.SecondDigitNotMatchedImagesDir);
                    secondCopyCount++;
                }
                else
                {
                    var depth = new ImageData
                    {
                        FirstDigit = firstDigitValue,
                        SecondDigit = secondDigitValue
                    };
                    var unixTime = imgPaths[i].Replace(_fileRepository.ImageDir, "").Replace("\\", "").Replace(".jpg", "");
                    depth.UnixTime = Convert.ToDouble(unixTime);
                    imageData.Add(depth);
                }
            }
            _logger.Information("First digit no match count: {0}", firstCopyCount);
            _logger.Information("Second digit no match count: {0}", secondCopyCount);
            return imageData;
        }

        //Copy not matched images for manual moving to learning data
        private void CopyNotMatchedImage(string imgPath, string notMatchedImagesDir)
        {
            var imageLocation = imgPath.Replace(_fileRepository.ImageDir, "");
            _fileRepository.CopyFileToNewLocation(imgPath, notMatchedImagesDir + imageLocation);
        }
    }
}
