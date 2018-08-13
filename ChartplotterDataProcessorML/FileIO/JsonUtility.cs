using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.FileIO
{
    public class JsonUtility
    {
        private readonly IFileRepository _fileRepository;

        public JsonUtility(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public IEnumerable<string> GetJson()
        {
            return _fileRepository.OpenAllFiles(_fileRepository.InputJsonDir, "json");
        }

        public void CreateJson(List<Data> collection)
        {
            var json = JsonConvert.SerializeObject(collection);
            json = "var data = " + json + ";"; //Create a JS array for now
            _fileRepository.WriteFile(_fileRepository.OutputJsonDir + "data.json", json);
        }

        public void CreateJson(string input)
        {
            _fileRepository.WriteFile(_fileRepository.OutputJsonDir, input);
        }
    }
}
