using ChartplotterDataProcessorML.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.UI
{
    public class ConfigChooser
    {
        private readonly IFileRepository _fileRepository;
        private readonly InputValidator _inputValidator;

        public ConfigChooser(IFileRepository fileRepository, InputValidator inputValidator)
        {
            _fileRepository = fileRepository;
            _inputValidator = inputValidator;
        }

        public string ChooseConfig()
        {
            Console.Clear();
            var configs = _fileRepository.GetAllFiles(_fileRepository.WaterLevelConfigDir, "cfg");
            ShowConfigs(configs);

            Console.Write("Config ID:");
            var id = _inputValidator.Validate(configs.Length);
            return configs[id - 1];
        }

        private void ShowConfigs(string[] configs)
        {
            Console.WriteLine("ID  CONFIG");
            for (int i = 0; i < configs.Length; i++)
            {
                Console.WriteLine(String.Format("{0, 2}", i + 1) + "  " + Path.GetFileName(configs[i]));
            }
        }
    }
}
