using System.Drawing;

namespace Pixelator.Api
{
    public class EmbeddedImage
    {
        public enum PixelStorage
        {
            Auto,
            High,
            Medium,
            Low
        }

        private readonly Image _image;
        private readonly PixelStorage _pixelStorage;

        public EmbeddedImage(Image image, PixelStorage pixelStorage)
        {
            _image = image;
            _pixelStorage = pixelStorage;
        }

        public Image Image
        {
            get { return _image; }
        }

        public PixelStorage EmbeddedPixelStorage
        {
            get { return _pixelStorage; }
        }
    }
}
