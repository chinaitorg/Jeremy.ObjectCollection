namespace Jeremy.ObjectCollectionSystem.DataTransferObjects;
/// <summary>
/// Kafka 消息主体
/// </summary>
public class KafkaMessageDTO
{
    public string SubFolder { get; set; } = null!;
    public long CollectionTime { get; set; }
    public string FolderCode { get; set; } = null!;
    public string Ip { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Folder { get; set; } = null!;
    public long CreateTime { get; set; }
    public string Name { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public string ImageType { get; set; } = null!;
}
