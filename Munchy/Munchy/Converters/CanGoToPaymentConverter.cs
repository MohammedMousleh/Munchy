﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Munchy.Converters
{
   public  class CanGoToPaymentConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           int count = (int)value;  
           
            if(count == 0)
            {
                return false; 
            }
          
            else
            {
                return true; 
            }
                }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
