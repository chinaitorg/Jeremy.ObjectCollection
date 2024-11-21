using System.Globalization;
using System.Windows.Data;

namespace Jeremy.ObjectCollectionSystem.Converters;

/// <summary>
/// 布尔转换器 
/// true - Y
/// false - N
/// </summary>
public class BooleanToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null && (bool) value ? "Y" : "N";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
