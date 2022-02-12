using System.Drawing;

namespace ImageCompressor.Interfaces
{
    public interface ICompressor
    {
        void CompressImage(Image sourceImage, int quality, string pathToSave);
        void CompressFromImagePath(string sourceImagePath, int imageQuality, string outputImagePath);
        void CompressFromImage(Image image, int imageQuality, string outputImagePath);
    }
}
