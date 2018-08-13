using SharpConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.FileIO
{
    public interface IFileRepository
    {
        string InputCsvDir { get; }
        string InputJsonDir { get; }
        string OutputCsvDir { get; }
        string OutputJsonDir { get; }
        string WaterLevelConfigDir { get; }
        string FirstDigitLearningImagesDir { get; }
        string SecondDigitLearningImagesDir { get; }
        string ImageDir { get; }
        string FirstDigitNotMatchedImagesDir { get; }
        string SecondDigitNotMatchedImagesDir { get; }

        IEnumerable<string> OpenAllFiles(string directory, string fileType);
        void WriteFile(string directory, string content);
        void WriteFile(string directory, IEnumerable<string> content);
        string[] FindFolders(string directory);
        string[] GetAllFiles(string directory, string fileType);
        string GetFile(string directory, string fileType);
        Configuration LoadConfiguration(string directory);
        List<Configuration> LoadConfigurations(string directory);
        void SaveConfiguration(Configuration config, string directory);
        Bitmap LoadImage(string path);
        void CopyFileToNewLocation(string file, string copyLocation);
    }
}
