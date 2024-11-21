using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Jeremy.ObjectCollectionSystem.Converters;

/// <summary>
/// 布尔转换器 
/// false - Visibility.Visible
/// true - Visibility.Collapsed
/// </summary>
public class BooleanToVisibilityReConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null && (bool) value ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

