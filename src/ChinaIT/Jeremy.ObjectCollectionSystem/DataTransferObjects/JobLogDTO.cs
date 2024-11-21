namespace Jeremy.ObjectCollectionSystem.DataTransferObjects;

public class JobLogDTO
{
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime StartTime { get; set; }
    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime EndTime { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }
}
