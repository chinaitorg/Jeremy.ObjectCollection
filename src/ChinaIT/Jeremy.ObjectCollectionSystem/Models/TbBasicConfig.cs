namespace Jeremy.ObjectCollectionSystem.Models;

public partial class TbBasicConfig
{
    public string Id { get; set; } = null!;
    public string Ip { get; set; } = null!;
    public string Mac { get; set; } = null!;
    public string DeviceNumber { get; set; } = null!;
    public string DeviceName { get; set; } = null!;
    public string? Comment { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public string CreateBy { get; set; } = null!;
    public DateTime? UpdateTime { get; set; }
    public string? UpdateBy { get; set; }
}
