using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.FileIO
{
    public class CsvUtility
    {
        private readonly IFileRepository _fileRepository;

        public CsvUtility(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public List<Data> GetCsv(string directory)
        {
            var AllFiles = _fileRepository.OpenAllFiles(directory, "csv");
            return ParseCsv(AllFiles);
        }

        public void CreateCsv(List<Data> objectList)
        {
            var CsvFormat = ParseToCsvFormat(objectList);
            _fileRepository.WriteFile(_fileRepository.OutputCsvDir + "data.csv", CsvFormat);
        }

        private List<Data> ParseCsv(IEnumerable<string> content)
        {
            return content
                .Where(line => line.Length > 60)
                .Select(Data.ParseString)
                .ToList();
        }

        private IEnumerable<string> ParseToCsvFormat<T>(IEnumerable<T> objectT)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var o in objectT)
            {
                yield return string.Join(",", fields.Select(f => (f.GetValue(o) ?? ""))
                    .Concat(properties.Select(p => (p.GetValue(o, null) ?? "")).ToArray()));
            }
        }
    }
}
