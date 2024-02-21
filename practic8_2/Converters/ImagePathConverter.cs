using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace practic8_2.Converters
{
    class ImagePathConverter :IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            var filename = value as string;

            if (filename == "нет")
            {
                return System.IO.Path.Combine(Environment.CurrentDirectory, "Images", "defaultphoto.jpg");
            }
            return System.IO.Path.Combine(Environment.CurrentDirectory, "Images", filename);
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
