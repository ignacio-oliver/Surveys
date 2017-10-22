using System;
using System.Globalization;
using Xamarin.Forms;

namespace Surveys.Core.BookCode
{
    public class BalanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var balance = (decimal)value;
            var color = Color.Green;

            if(balance >= 5000 && balance <= 10000)
            {
                color = Color.Orange;
            }
            else if (balance > 10000)
            {
                color = Color.Red;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
