namespace Jeremy.ObjectCollectionSystem.Converters;

public class StringToStyleConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string name = value.ToString();
        return name switch
        {
            "BadgeWarning" => "#e9af20", // 警告
            "BadgeSuccess" => "#2db84d", // 成功
            "BadgeDanger" => "#db3340", // 危险
            _ => "#e9af20",
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string name = value.ToString();
        return name switch
        {
            "#e9af20" => "BadgeWarning",
            "#2db84d" => "BadgeSuccess",
            "#db3340" => "BadgeDanger",
            _ => (object) "BadgeDanger",
        };
    }
}

