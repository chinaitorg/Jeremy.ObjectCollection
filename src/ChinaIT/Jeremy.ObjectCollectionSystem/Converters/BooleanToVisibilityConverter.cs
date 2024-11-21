using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Jeremy.ObjectCollectionSystem.Converters;

/// <summary>
/// 布尔转换器 
/// true - Visibility.Visible
/// false - Visibility.Collapsed
/// </summary>
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null && (bool) value ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

