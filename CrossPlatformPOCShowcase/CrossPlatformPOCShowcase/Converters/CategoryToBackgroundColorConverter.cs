using CrossPlatformPOCShowcase.Core.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace CrossPlatformPOCShowcase.Converters
{
    public class CategoryToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.White;

            if (!Enum.TryParse(value.ToString(), out ItemCategory enumValue))
                return Color.White;

            switch (enumValue)
            {
                case ItemCategory.Shopping:
                    return Color.FromRgba(255, 0, 0, 0.1);
                case ItemCategory.Work:
                    return Color.FromRgba(0, 0, 255, 0.1);
                case ItemCategory.Personal:
                    return Color.FromRgba(0, 255, 0, 0.1);
                default:
                    return Color.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
