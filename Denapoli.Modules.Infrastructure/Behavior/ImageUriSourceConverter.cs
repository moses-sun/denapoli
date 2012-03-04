using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Denapoli.Modules.Infrastructure.Behavior
{
    public class ImageUriSourceConverter : IValueConverter
    {
        private static readonly Dictionary<string, BitmapImage> Images = new Dictionary<string, BitmapImage>();
        private static BitmapImage _unknown = DownloadImage("unknown.png");
        private const string HostName = "http://127.0.0.1:8080/images/";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if(!Images.ContainsKey(value.ToString()))
            {
                Images[value.ToString()] = DownloadImage(value.ToString());
            }
            return Images[value.ToString()];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static BitmapImage DownloadImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName)) return _unknown;

            var image = new BitmapImage();
            var request = WebRequest.Create(new Uri(HostName + imageName, UriKind.Absolute));
            request.Timeout = -1;
            WebResponse response;
            try
            {
                response = request.GetResponse();
            }
            catch(Exception)
            {
                return _unknown;
            }
            var responseStream = response.GetResponseStream();
            var reader = new BinaryReader(responseStream);
            var memoryStream = new MemoryStream();

            var bytebuffer = new byte[response.ContentLength];
            int bytesRead = reader.Read(bytebuffer, 0, (int) response.ContentLength);

            while (bytesRead > 0)
            {
                memoryStream.Write(bytebuffer, 0, bytesRead);
                bytesRead = reader.Read(bytebuffer, 0, (int) response.ContentLength);
            }

            image.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);

            image.StreamSource = memoryStream;
            image.EndInit();
            return image;
        }
    }
}