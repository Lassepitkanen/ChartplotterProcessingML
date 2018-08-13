using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartplotterDataProcessorML.FileIO;
using ChartplotterDataProcessorML.UI;

namespace ChartplotterDataProcessorML
{
    public class ProcessingContextLoader
    {
        private readonly FolderChooser _folderChooser;
        private readonly LocatioSettingsLoader _configLoader;
        private readonly ConfigChooser _configChooser;
        private readonly CsvUtility _csvUtility;

        public ProcessingContextLoader(FolderChooser folderChooser, LocatioSettingsLoader configLoader, ConfigChooser configChooser, CsvUtility csvUtility)
        {
            _folderChooser = folderChooser;
            _configLoader = configLoader;
            _configChooser = configChooser;
            _csvUtility = csvUtility;
        }

        public void LoadContext(ProcessingContext context)
        {
            var directory = _folderChooser.ChooseFolder();
            var configDir = _configChooser.ChooseConfig();

            context.Rows = _csvUtility.GetCsv(directory);
            context.LocationSettings = _configLoader.LoadLocationSettings(configDir);
        }
    }
}
