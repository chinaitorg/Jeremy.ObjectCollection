namespace Jeremy.ObjectCollectionSystem.Models;

public partial class TbJobConfig
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int ThreadCount { get; set; }
    public int ScanIntervalType { get; set; }
    public string ScanInterval { get; set; } = null!;
    public string ScanPath { get; set; } = null!;
    public bool IsDelete { get; set; } = false;
    public bool IsRunning { get; set; } = false;
    public string? Comment { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public string CreateBy { get; set; } = null!;
    public DateTime? UpdateTime { get; set; }
    public string? UpdateBy { get; set; }
}
