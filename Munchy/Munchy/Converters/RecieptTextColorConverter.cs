using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Munchy.Converters
{
    public class RecieptTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valueAsString = bool.Parse(value.ToString());
            switch (valueAsString)
            {
                case (true):
                    {
                        return Color.Green;
                    }
                case (false):
                    {
                        return Color.Red;
                    }
                default:
                    {
                        return Color.Black;
                    }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
