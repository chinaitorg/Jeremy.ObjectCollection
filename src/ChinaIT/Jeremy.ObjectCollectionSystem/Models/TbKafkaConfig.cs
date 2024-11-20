namespace Jeremy.ObjectCollectionSystem.Models;

public partial class TbKafkaConfig
{
    public string Id { get; set; } = null!;
    public string BootstrapServers { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public string? Comment { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public string CreateBy { get; set; } = null!;
    public DateTime? UpdateTime { get; set; }
    public string? UpdateBy { get; set; }
}

