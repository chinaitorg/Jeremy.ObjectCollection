namespace Jeremy.ObjectCollectionSystem.Domains;


public static class GlobalInfo
{
    /// <summary>
    /// 主题
    /// </summary>
    public static ApplicationTheme ApplicationTheme { get; set; } = ApplicationTheme.Dark;
    /// <summary>
    /// IP 地址
    /// </summary>
    public static string Ip { get; set; } = null!;
    /// <summary>
    /// Mac 地址
    /// </summary>
    public static string Mac { get; set; } = null!;
    /// <summary>
    /// 设备编码
    /// </summary>
    public static string DeviceNumber { get; set; } = null!;
    /// <summary>
    /// 设备名称
    /// </summary>
    public static string DeviceName { get; set; } = null!;
    /// <summary>
    /// MinIO 终结点
    /// </summary>
    public static string EndPoint { get; set; } = null!;
    /// <summary>
    /// MinIO 用户名
    /// </summary>
    public static string AccessKey { get; set; } = null!;
    /// <summary>
    /// MinIO 密码
    /// </summary>
    public static string SecretKey { get; set; } = null!;
    /// <summary>
    /// MinIO 桶名称
    /// </summary>
    public static string BucketName { get; set; } = null!;
    /// <summary>
    /// MinIO 路径
    /// </summary>
    public static string MinIOPath { get; set; } = null!;
    /// <summary>
    /// Kafka 集群地址
    /// </summary>
    public static string BootstrapServers { get; set; } = null!;
    /// <summary>
    /// Topic 名称
    /// </summary>
    public static string Topic { get; set; } = null!;
    /// <summary>
    /// 计划任务主键名称
    /// </summary>
    public static string JobId { get; set; } = null!;

    /// <summary>
    /// 计划任务名称
    /// </summary>
    public static string JobName { get; set; } = null!;
    /// <summary>
    /// 扫描周期
    /// </summary>
    public static string ScanInterval { get; set; } = "1000";
    /// <summary>
    /// 是否删除源文件
    /// </summary>
    public static bool IsDeleteFile { get; set; } = false;
    /// <summary>
    /// 扫描路径
    /// </summary>
    public static string ScanPath { get; set; } = null!;
    /// <summary>
    /// 是否采用增量采集策略 Y-是 N-否
    /// </summary>
    public static string IsIncrementalCollection { get; set; } = ConfigHelper.GetAppSetting("IsIncrementalCollection");
}

