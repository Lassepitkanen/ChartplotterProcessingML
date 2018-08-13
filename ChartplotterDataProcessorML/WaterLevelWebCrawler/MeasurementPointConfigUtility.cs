using ChartplotterDataProcessorML.FileIO;
using SharpConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.WaterLevelWebCrawler
{
    public interface IMeasurementPointUtility
    {
        List<MeasurementPoint> LoadMeasurementPoints();
        void SaveMeasurementPoints(string location, List<WaterLevelData> data);
    }

    public class MeasurementPointConfigUtility : IMeasurementPointUtility
    {
        private readonly TimeConverter _timeConverter;
        private readonly IFileRepository _fileRepository;

        public MeasurementPointConfigUtility(TimeConverter timeConverter, IFileRepository fileRepository)
        {
            _timeConverter = timeConverter;
            _fileRepository = fileRepository;
        }

        public List<MeasurementPoint> LoadMeasurementPoints()
        {
            var config = _fileRepository.LoadConfiguration("appconfig.cfg");
            var section = config["MeasurementPoints"];

            var measurementPoints = new List<MeasurementPoint>();
            foreach (var line in section)
            {
                measurementPoints.Add(new MeasurementPoint() { Location = line.Name, Url = line.StringValue });
            }
            return measurementPoints;
        }

        public void SaveMeasurementPoints(string location, List<WaterLevelData> data)
        {
            var configFileDir = _fileRepository.WaterLevelConfigDir + location + ".cfg";
            if (File.Exists(configFileDir))
            {
                var configFile = _fileRepository.LoadConfiguration(configFileDir);
                foreach (var item in data)
                {
                    var date = _timeConverter.ConvertToUnixTime(item.Year, item.Month, item.Day, item.Hour, 0, 0).ToString();
                    if (configFile["WaterLevel"][date].IsEmpty)
                    {
                        configFile["WaterLevel"][date].DoubleValue = item.WaterLevel;
                    }
                }
                _fileRepository.SaveConfiguration(configFile, configFileDir);
            }
            else //Config not found, creating one
            {
                var configFile = new Configuration();
                configFile["DefaultWaterLevel"]["DefaultWaterLevel"].DoubleValue = 0;
                foreach (var item in data)
                {
                    var date = _timeConverter.ConvertToUnixTime(item.Year, item.Month, item.Day, item.Hour, 0, 0).ToString();
                    configFile["WaterLevel"][date].DoubleValue = item.WaterLevel;
                }
                _fileRepository.SaveConfiguration(configFile, configFileDir);
            }
        }
    }
}
