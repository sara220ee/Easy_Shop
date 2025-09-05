using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using EasyShopWindow;

namespace EasyShopWindow
{
    public class StockToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int quantity)
            {
                if (parameter as string == "Foreground")
                {
                    return quantity > 0 ? "#FF4CAF50" : "#FFf44336";
                }
                return quantity > 0 ? $"En stock: {quantity}" : "Stock épuisé";
            }
            return "Stock inconnu";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}