using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.WaterLevelWebCrawler
{
    public class StringEditUtility
    {
        public List<WaterLevelData> GetWaterLevelData(string strSource)
        {
            var unEditedString = GetBetween(strSource, "var nykytila = [", "];");
            var csvFormatString = RemoveUnnecessaryChars(unEditedString);
            return Parse(csvFormatString);
        }

        private string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        private string[] RemoveUnnecessaryChars(string str)
        {
            return str
                .Replace(" ", "")
                .Replace("[Date.UTC(", "")
                .Replace(")", "")
                .Replace("]", "")
                .Split(new [] { "\n" }, StringSplitOptions.None);
        }

        private List<WaterLevelData> Parse(IEnumerable<string> content)
        {
            return content
                .Where(line => line.Length > 2)
                .Select(WaterLevelData.ParseString)
                .ToList();
        }
    }
}
