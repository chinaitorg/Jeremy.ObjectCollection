using System.Configuration;

namespace Jeremy.ObjectCollectionSystem.Domains;

public class ConfigHelper
{
    private static readonly Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

    /// <summary>
    /// 根据关键字获取配置信息
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetAppSetting(string key)// => ConfigurationManager.AppSettings[key];
    {
        //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (config.AppSettings.Settings[key] != null)
            return config.AppSettings.Settings[key].Value;
        else
            return string.Empty;
    }
    /// <summary>
    /// 设置配置文件
    /// </summary>
    /// <param name="section"></param>
    public static void SetAppSetting(string key, string value)
    {
        //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (config.AppSettings.Settings[key] != null)
            config.AppSettings.Settings[key].Value = value;
        else
            config.AppSettings.Settings.Add(key, value);
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }
}
