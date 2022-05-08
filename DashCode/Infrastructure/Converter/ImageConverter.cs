using DashCode.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DashCode.Infrastructure.Converter
{
    public class ImageConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is byte[] photo)
            {
                try
                {
                    return ImageTools.ByteArrToBitmapImage(photo);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Load image error", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new InvalidOperationException("Load image error");
                }
            }
            return new BitmapImage(new Uri(@"/Resources/user_no_login.png", UriKind.Relative));
        }
    }
}
