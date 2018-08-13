using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition.Models
{
    public interface IModelMatcher
    {
        int MatchModel(bool[] predicate);
    }
}
