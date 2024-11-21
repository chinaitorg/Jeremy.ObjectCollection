using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Jeremy.ObjectCollectionSystem.Converters;

/// <summary>
/// NA转换器 
/// NA - Visibility.Visible
/// 其它 - Visibility.Collapsed
/// </summary>
public class NAToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != "0.01" ? Visibility.Visible : Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
