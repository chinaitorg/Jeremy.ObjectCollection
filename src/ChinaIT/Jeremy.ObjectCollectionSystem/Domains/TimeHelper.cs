namespace Jeremy.ObjectCollectionSystem.Domains;

public class TimeHelper
{
    /// <summary>
    /// Unix 时间戳转换成时间
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public static DateTime ConvertTimestampToDate(long timestamp)
    {
        DateTime defaultTime = new(1970, 1, 1, 0, 0, 0);
        return new DateTime(defaultTime.Ticks + timestamp * 10000).AddHours(8);
    }

    /// <summary>
    /// 时间转换成 Unix 时间戳
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ConvertToTimestamp(DateTime dateTime)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
        return (long) (dateTime - startTime).TotalMilliseconds;
    }
}
