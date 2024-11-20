namespace Jeremy.ObjectCollectionSystem.Models;

public partial class TbMinioConfig
{
    public string Id { get; set; } = null!;
    public string EndPoint { get; set; } = null!;
    public string AccessKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string? Comment { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public string CreateBy { get; set; } = null!;
    public DateTime? UpdateTime { get; set; }
    public string? UpdateBy { get; set; }
}

