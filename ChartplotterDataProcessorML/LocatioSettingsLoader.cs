using ChartplotterDataProcessorML.FileIO;
using SharpConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML
{
    public class LocatioSettingsLoader
    {
        private readonly IFileRepository _fileRepository;

        public LocatioSettingsLoader(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public LocationSettings LoadLocationSettings(string directory)
        {
            var config = _fileRepository.LoadConfiguration(directory);
            var section = config["DefaultWaterLevel"];
            var settings = new LocationSettings();
            try
            {
                settings.DefaultWaterLevel = section["DefaultWaterLevel"].DoubleValue;
            }
            catch (Exception)
            {
                Console.WriteLine("missing section: DefaultWaterLevel in waterlevelconfig");
            }

            section = config["WaterLevel"];
            var waterLevels = new List<MeasurementPoint>();
            foreach (var line in section)
            {
                waterLevels.Add(new MeasurementPoint { UnixTime = Convert.ToInt64(line.Name), WaterLevel = line.DoubleValue });
            }
            settings.MeasurementPoint = waterLevels;

            settings.Shoreline1 = LoadShoreLine(config, "Shoreline1");
            settings.Shoreline2 = LoadShoreLine(config, "Shoreline2");

            return settings;
        }

        private List<Shoreline> LoadShoreLine(Configuration config, string shoreLineNum)
        {
            var shoreLine = new List<Shoreline>();
            try
            {
                var section = config[shoreLineNum];
                foreach (var line in section)
                {
                    shoreLine.Add(new Shoreline() { GpsLat = line.DecimalValueArray[0], GpsLng = line.DecimalValueArray[1] });
                }
            }
            catch (Exception)
            {
                Console.WriteLine("missing section: {0}", shoreLineNum);
            }
            return shoreLine;
        }
    }
}
