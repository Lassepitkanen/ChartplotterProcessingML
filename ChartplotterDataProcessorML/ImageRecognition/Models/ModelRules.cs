using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition.Models
{
    public interface IModelRule
    {
        bool IsMatch(bool[] predicted);
        int ShowValue();
    }

    public class ModelRule : IModelRule
    {
        private readonly int _ruleNumber;

        public ModelRule(int ruleNumber)
        {
            _ruleNumber = ruleNumber;
        }

        public bool IsMatch(bool[] predicted)
        {
            if (predicted[_ruleNumber])
                return true;
            else
                return false;
        }

        public int ShowValue()
        {
            return _ruleNumber;
        }
    }

    public class ModelRuleNoMatch : IModelRule
    {
        public bool IsMatch(bool[] predicted)
        {
            return true;
        }

        public int ShowValue()
        {
            return -1;
        }
    }
}
