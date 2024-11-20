namespace Jeremy.ObjectCollectionSystem.Models;

public partial class TbJobLog
{
    public string Id { get; set; } = null!;
    public string JobId { get; set; } = null!;
    public string DeviceNumber { get; set; } = null!;
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public string CreateBy { get; set; } = null!;
    public string OriginFilefullPath { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string? FileType { get; set; }
    public bool IsMinIo { get; set; }
    public DateTime? IsMinIotime { get; set; }
    public string? MinIobucketName { get; set; }
    public string? MinIopath { get; set; }
    public string? MinIofileName { get; set; }
    public string? MinIourl { get; set; }
    public bool IsKafka { get; set; }
    public DateTime? IsKafkaTime { get; set; }
    public string? KafkaTopic { get; set; }
    public string? KafkaBody { get; set; }
    public string? Comment { get; set; }
}

