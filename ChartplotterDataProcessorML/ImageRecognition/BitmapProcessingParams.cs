using SharpConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition
{
    public class BitmapProcessingParams
    {
        public Rectangle FirstDigitRectangle { get; }
        public Rectangle SecondDigitRectangle { get; }
        public int ResizeWidth { get; }
        public int ResizeHeight { get; }

        public BitmapProcessingParams()
        {
            FirstDigitRectangle = new Rectangle(135, 290, 100, 130);
            SecondDigitRectangle = new Rectangle(250, 290, 100, 130);

            ResizeWidth = 40;
            ResizeHeight = 52;

            if (File.Exists("appconfig.cfg"))
            {
                try
                {
                    var config = Configuration.LoadFromFile("appconfig.cfg");
                    var section = config["BitmapProcessingParams"];

                    var firstRectangleX = section["FirstRectangleX"].IntValue;
                    var firstRectangleY = section["FirstRectangleY"].IntValue;
                    var firstRectangleWidth = section["FirstRectangleWidth"].IntValue;
                    var firstRectangleHeight = section["FirstRectangleHeight"].IntValue;
                    var secondRectangleX = section["SecondRectangleX"].IntValue;
                    var secondRectangleY = section["SecondRectangleY"].IntValue;
                    var secondRectangleWidth = section["SecondRectangleWidth"].IntValue;
                    var secondRectangleHeight = section["SecondRectangleHeight"].IntValue;
                    FirstDigitRectangle = new Rectangle(firstRectangleX, firstRectangleY, firstRectangleWidth, firstRectangleHeight);
                    SecondDigitRectangle = new Rectangle(secondRectangleX, secondRectangleY, secondRectangleWidth, secondRectangleHeight);

                    ResizeWidth = section["ResizeWidth"].IntValue;
                    ResizeHeight = section["ResizeHeight"].IntValue;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
