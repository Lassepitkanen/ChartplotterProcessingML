using SharpConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ChartplotterDataProcessorML.FileIO
{
    class FileRepository : IFileRepository
    {
        public string InputCsvDir { get; }
        public string InputJsonDir { get; }
        public string OutputCsvDir { get; }
        public string OutputJsonDir { get; }
        public string WaterLevelConfigDir { get; }

        public string FirstDigitLearningImagesDir { get; }
        public string SecondDigitLearningImagesDir { get; }
        public string ImageDir { get; }
        public string FirstDigitNotMatchedImagesDir { get; }
        public string SecondDigitNotMatchedImagesDir { get; }

        public FileRepository()
        {
            InputCsvDir = "data/csv";
            InputJsonDir = "data/json";
            OutputCsvDir = "data/processed/csv/";
            OutputJsonDir = "data/processed/json/";
            WaterLevelConfigDir = "data/locationconfigs/";

            FirstDigitLearningImagesDir = "data/learningimages/firstdigit/";
            SecondDigitLearningImagesDir = "data/learningimages/seconddigit/";
            FirstDigitNotMatchedImagesDir = "data/nomatchimages/firstdigit/";
            SecondDigitNotMatchedImagesDir = "data/nomatchimages/seconddigit/";
            ImageDir = "data/images/";

            if (File.Exists("appconfig.cfg"))
            {
                try
                {
                    var config = Configuration.LoadFromFile("appconfig.cfg");
                    var section = config["FileRepository"];

                    InputCsvDir = section["InputCsvDir"].StringValue;
                    InputJsonDir = section["InputJsonDir"].StringValue;
                    OutputCsvDir = section["OutputCsvDir"].StringValue;
                    OutputJsonDir = section["OutputJsonDir"].StringValue;
                    WaterLevelConfigDir = section["WaterLevelConfigDir"].StringValue;

                    FirstDigitLearningImagesDir = section["FirstDigitLearningImagesDir"].StringValue;
                    SecondDigitLearningImagesDir = section["SecondDigitLearningImagesDir"].StringValue;
                    ImageDir = section["ImageDir"].StringValue;
                    FirstDigitNotMatchedImagesDir = section["FirstDigitNotMatchedImagesDir"].StringValue;
                    SecondDigitNotMatchedImagesDir = section["SecondDigitNotMatchedImagesDir"].StringValue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public string[] FindFolders(string directory)
        {
            string[] folders = null;
            try
            {
                folders = Directory.GetDirectories(directory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return folders;
        }

        public IEnumerable<string> OpenAllFiles(string directory, string filetype)
        {
            var AllFiles = GetAllFiles(directory, filetype);
            return WriteFilesToString(AllFiles);
        }

        public void WriteFile(string directory, IEnumerable<string> content)
        {
            try
            {
                File.WriteAllLines(directory, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void WriteFile(string directory, string content)
        {
            try
            {
                File.WriteAllText(directory, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string[] GetAllFiles(string directory, string fileType)
        {
            return Directory.GetFiles(directory, "*." + fileType, SearchOption.AllDirectories);
        }

        public string GetFile(string directory, string fileType)
        {
            return Directory.GetFiles(directory, "*." + fileType).ToString();
        }

        public Configuration LoadConfiguration(string directory)
        {
            return Configuration.LoadFromFile(directory);
        }

        public List<Configuration> LoadConfigurations(string directory)
        {
            var cfgDirectories = GetAllFiles(directory, "cfg");

            var configs = new List<Configuration>();
            foreach (var item in cfgDirectories)
            {
                var config = Configuration.LoadFromFile(item);
                configs.Add(config);
            }
            return configs;
        }

        public void SaveConfiguration(Configuration config, string directory)
        {
            try
            {
                config.SaveToFile(directory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public Bitmap LoadImage(string path)
        {
            return new Bitmap(path);
        }

        public void CopyFileToNewLocation(string file, string copyLocation)
        {
            try
            {
                File.Copy(file, copyLocation, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private IEnumerable<string> WriteFilesToString(string[] files)
        {
            return files.SelectMany(p => File.ReadAllLines(Path.Combine(p)));
        }
    }
}
