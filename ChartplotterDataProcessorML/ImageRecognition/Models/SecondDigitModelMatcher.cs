using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChartplotterDataProcessorML.ImageRecognition.Models
{
    public class SecondDigitModelMatcher : IModelMatcher
    {
        private readonly IEnumerable<IModelRule> _modelRules;

        public SecondDigitModelMatcher(IEnumerable<IModelRule> modelRules)
        {
            _modelRules = modelRules;
        }

        public int MatchModel(bool[] predicate)
        {
            return _modelRules.First(r => r.IsMatch(predicate)).ShowValue();
        }
    }
}
