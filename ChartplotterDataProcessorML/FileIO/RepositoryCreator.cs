using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChartplotterDataProcessorML.FileIO
{
    public class RepositoryCreator 
    {
        private readonly IFileRepository _fileRepository;

        public RepositoryCreator(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public void CreateFolders()
        {
            Directory.CreateDirectory(_fileRepository.InputCsvDir);
            Directory.CreateDirectory(_fileRepository.InputJsonDir);
            Directory.CreateDirectory(_fileRepository.OutputJsonDir);
            Directory.CreateDirectory(_fileRepository.OutputCsvDir);

            Directory.CreateDirectory(_fileRepository.WaterLevelConfigDir);

            Directory.CreateDirectory(_fileRepository.FirstDigitLearningImagesDir);
            Directory.CreateDirectory(_fileRepository.SecondDigitLearningImagesDir);
            Directory.CreateDirectory(_fileRepository.FirstDigitNotMatchedImagesDir);
            Directory.CreateDirectory(_fileRepository.SecondDigitNotMatchedImagesDir);
            Directory.CreateDirectory(_fileRepository.ImageDir);
        }
    }
}
