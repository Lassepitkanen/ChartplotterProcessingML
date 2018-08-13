using ChartplotterDataProcessorML.ImageRecognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.Tasks
{
    public class ImageRecognitionTask : ITask
    {
        private ImageRecognitionMain _imageRecognitionMain;

        public string Description => TaskName.ImageRecognition;

        public ImageRecognitionTask(ImageRecognitionMain imageRecognitionMain)
        {
            _imageRecognitionMain = imageRecognitionMain;
        }

        public void Run(ProcessingContext context)
        {
            _imageRecognitionMain.Run(context);

            Console.Clear();
            Console.WriteLine("Image recognition complete");
        }
    }
}
