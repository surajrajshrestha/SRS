using ImageCompressor.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageCompressor.Implementations
{
    public class Compressor : ICompressor
    {
        public Compressor()
        {
        }

        public void CompressImage(Image sourceImage, int quality, string pathToSave)
        {
            try
            {
                ImageCodecInfo jpegCodec = ImageCodecInfo.GetImageEncoders().Where(m => m.MimeType == "image/jpeg").First();
                EncoderParameter imageQualityParameter = new(Encoder.Quality, quality);
                ImageCodecInfo[] allCodecs = ImageCodecInfo.GetImageEncoders();
                EncoderParameters codecParameters = new EncoderParameters(1);
                codecParameters.Param[0] = imageQualityParameter;

                for (int i = 0; i < allCodecs.Length; i++)
                {
                    if (allCodecs[i].MimeType == "image/jpeg")
                    {
                        jpegCodec = allCodecs[i];
                        break;
                    }
                }
                File.Delete(pathToSave);
                sourceImage.Save(pathToSave, jpegCodec, codecParameters);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void CompressFromImagePath(string inputImagePath, int imageQuality, string outputImagePath)
        {
            try
            {
                Bitmap source = new Bitmap(inputImagePath);
                CompressImage(source, imageQuality, outputImagePath);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void CompressFromImage(Image image, int imageQuality, string outputImagePath)
        {
            try
            {
                Bitmap source = new Bitmap(image);
                CompressImage(source, imageQuality, outputImagePath);

            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CompressImage(Bitmap bitmapImage, int imageQuality, string outputImagePath)
        {
            try
            {
                using var graphics = Graphics.FromImage(bitmapImage);
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                graphics.DrawImage(bitmapImage, 0, 0, bitmapImage.Width, bitmapImage.Height);

                using var output = File.Open(outputImagePath, FileMode.Create);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, imageQuality);
                var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Png.Guid);

                bitmapImage.Save(output, codec, encoderParameters);
                output.Close();

                graphics.Dispose();
                bitmapImage.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
