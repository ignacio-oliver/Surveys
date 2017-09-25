﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Surveys.Core
{
    public class TeamColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return null;
            }

            var team = (string)value;
            var color = Color.Transparent;

            switch(team)
            {
                case "América":
                    color = Color.Yellow;
                    break;
                case "Boca Juniors":
                case "Colo-Colo":
                case "Alianza Lima":
                    color = Color.Blue;
                    break;
                case "Caracas":
                case "Saprissa":
                    color = Color.Purple;
                    break;
                case "Real Madrid":
                    color = Color.Fuchsia;
                    break;
                case "Nacional":
                    color = Color.Red;
                    break;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
