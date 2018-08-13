using ChartplotterDataProcessorML.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition
{
    public class ImageLoader
    {
        private readonly IFileRepository _fileRepository;
        private readonly BitmapProcessor _bitmapProcessor;
        private readonly BitmapProcessingParams _bitmapProcessingParams;

        public ImageLoader(IFileRepository fileRepository, BitmapProcessingParams bitmapProcessingParams, BitmapProcessor bitmapProcessor )
        {
            _fileRepository = fileRepository;
            _bitmapProcessingParams = bitmapProcessingParams;
            _bitmapProcessor = bitmapProcessor;
        }

        public Bitmap[] LoadLearningImages(IEnumerable<string[]> learningImgPaths, Rectangle digitRectangle)
        {
            var totalBitmaps = learningImgPaths.Select(x => x.Count()).Sum();          
            var learningBitmaps = new Bitmap[totalBitmaps];
            var bitmapsInserted = 0;
            foreach (var Paths in learningImgPaths)
            {
                var bitmaps = (LoadSingleDigitImages(Paths, digitRectangle));
                for (int i = 0; i < bitmaps.Length; i++)
                {
                    learningBitmaps[bitmapsInserted +i] = bitmaps[i];
                }
                bitmapsInserted = bitmapsInserted + bitmaps.Length;
            }
            return learningBitmaps;
        }

        public Bitmap[][] LoadTwoDigitImages(string[] imgPaths)
        {
            var processedBitmaps = new Bitmap[][] { new Bitmap[imgPaths.Length], new Bitmap[imgPaths.Length] };
            for (int i = 0; i < imgPaths.Length; i++)
            {
                var bitmap = _fileRepository.LoadImage(imgPaths[i]);
                processedBitmaps[0][i] = _bitmapProcessor.Process(bitmap, _bitmapProcessingParams.FirstDigitRectangle);
                processedBitmaps[1][i] = _bitmapProcessor.Process(bitmap, _bitmapProcessingParams.SecondDigitRectangle);
            }
            return processedBitmaps;
        }

        public IEnumerable<string[]> LoadModelFolders(string directory)
        {
            return _fileRepository.FindFolders(directory)
                .Select(folder => _fileRepository.GetAllFiles(folder, "jpg"));
        }

        public Bitmap[] LoadSingleDigitImages(string[] imgPaths, Rectangle rectangle)
        {
            return imgPaths.Select(path => _fileRepository.LoadImage(path))
                .Select(img => _bitmapProcessor.Process(img, rectangle)).ToArray(); 
        }
    }
} 