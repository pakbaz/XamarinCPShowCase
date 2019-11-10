using CrossPlatformPOCShowcase.Core.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace CrossPlatformPOCShowcase.Converters
{
    public class CategoryToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.Black;

            if (!Enum.TryParse(value.ToString(), out ItemCategory enumValue))
                return Color.Black;

            switch (enumValue)
            {
                case ItemCategory.Shopping:
                    return Color.Red;
                case ItemCategory.Work:
                    return Color.Blue;
                case ItemCategory.Personal:
                    return Color.Green;
                default:
                    return Color.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
