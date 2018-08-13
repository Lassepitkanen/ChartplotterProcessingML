using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.UI
{
    public class InputValidator
    {
        public int Validate(int range)
        {
            string inputLine = Console.ReadLine();
            int inputInteger;

            while (!Int32.TryParse(inputLine, out inputInteger) || inputInteger > range || inputInteger < 1)
            {
                Console.WriteLine("Not valid input, try again");
                inputLine = Console.ReadLine();
            }   
            return inputInteger;
        }
    }
}
