using System.Windows.Data;

namespace Jeremy.ObjectCollectionSystem.Converters;

public class ShortToStyleStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string name = value.ToString();
        return name switch
        {
            "0" => "#2db84d", // 成功
            "1" => "#db3340", // 危险
            "2" => "#e9af20", // 警告
            _ => "#e9af20",
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string name = value.ToString();
        return name switch
        {
            "#2db84d" => 0,
            "#db3340" => 1,
            "#e9af20" => 2,
            _ => (object) 0,
        };
    }
}

