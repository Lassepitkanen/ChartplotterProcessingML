using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.ImageRecognition
{
    public class BitmapProcessor
    {
        private readonly Crop _crop;
        private readonly Grayscale _grayscale;
        private readonly ContrastStretch _contrastStretch;
        private readonly ResizeBilinear _resizeBilinear;
        private readonly BitmapProcessingParams _bitmapProcessingParams;

        public BitmapProcessor(Crop crop, Grayscale grayscale, ContrastStretch contrastStretch, ResizeBilinear resizeBilinear, BitmapProcessingParams bitmapProcessingParams)
        {
            _crop = crop;
            _grayscale = grayscale;
            _contrastStretch = contrastStretch;
            _resizeBilinear = resizeBilinear;
            _bitmapProcessingParams = bitmapProcessingParams;

            _resizeBilinear.NewWidth = _bitmapProcessingParams.ResizeWidth;
            _resizeBilinear.NewHeight = _bitmapProcessingParams.ResizeHeight;
        }

        public Bitmap Process(Bitmap bitmap, Rectangle rectangle)
        {
            var croppedImg = Crop(bitmap, rectangle);
            var resizedImg = Resize(croppedImg);
            var contrastImg = ApplyContrastStrect(resizedImg);
            return RemoveColors(contrastImg);
        }

        private Bitmap Crop(Bitmap bitmap, Rectangle rectangle)
        {
            _crop.Rectangle = rectangle;
            return _crop.Apply(bitmap);
        }

        private Bitmap Resize(Bitmap bitmap)
        {
            return _resizeBilinear.Apply(bitmap);
        }

        private Bitmap RemoveColors(Bitmap bitmap)
        {
            return _grayscale.Apply(bitmap);
        }

        private Bitmap ApplyContrastStrect(Bitmap bitmap)
        {
            return _contrastStretch.Apply(bitmap);
        }
    }
}
