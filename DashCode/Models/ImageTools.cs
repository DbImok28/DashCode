using System;
using System.IO;
using System.Reflection;

namespace DashCode.Models
{
    public static class ImageTools
    {
        public static byte[] ImageToByteArr(System.Drawing.Image x)
        {
            var imageConverter = new System.Drawing.ImageConverter();
            byte[] xByte = (byte[])imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }
        public static byte[] ImageToByteArr(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) return null;
            return ImageToByteArr(System.Drawing.Image.FromFile(imagePath));
        }
        public static System.Drawing.Image ByteArrToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return System.Drawing.Image.FromStream(ms);
            }
        }
        public static System.Windows.Media.Imaging.BitmapImage ByteArrToBitmapImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                var image = new System.Windows.Media.Imaging.BitmapImage();
                image.BeginInit();
                image.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
        public static System.Windows.Media.Imaging.BitmapImage LoadBitmapFromResource(string pathInApplication, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            if (pathInApplication[0] == '/')
            {
                pathInApplication = pathInApplication.Substring(1);
            }
            return new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/" + assembly.GetName().Name + ";component/" + pathInApplication, UriKind.Absolute));
        }
    }
}
