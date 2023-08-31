using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Game_Center.Projects.MusicProject.Models
{
    internal class StringtoBoolConvert:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string selectedOption = value as string;
            string optionToCompare = parameter as string;
            return selectedOption == optionToCompare;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                // The radio button is checked, return the corresponding option string
                return parameter as string;
            }

            // The radio button is not checked, return DependencyProperty.UnsetValue
            // to indicate that no value should be updated in the source property
            return DependencyProperty.UnsetValue;
        }
    }
}
