using System.Windows.Data;

namespace Jeremy.ObjectCollectionSystem.Converters;


/// <summary>
/// 布尔样式转换器
/// </summary>
public class BooleanToStyleStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        bool name = (bool) value;
        return name switch
        {
            true => "#2db84d", // 成功
            _ => "#db3340", // 危险
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string name = value.ToString();
        return name switch
        {
            "#2db84d" => true,
            _ => false,
        };
    }
}

