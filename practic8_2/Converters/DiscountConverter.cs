using practic8_2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace practic8_2.Converters
{
    class DiscountConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is Product product)
            {
                if (product.Discount.HasValue && product.Discount.Value > 0)
                {
                    double newPrice = product.Price * (1 - product.Discount.Value);
                    return $"Обычная цена: {product.Price:0.00} рублей\nЦена со скидкой: {newPrice:0.00} рублей";
                }
                else
                {
                    return $"{product.Price:0.00} рублей";
                }
            }
            return Binding.DoNothing;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
