namespace Arqom.Extensions.Storage.Abstractions;

public class FileMetadata
{
    public string Name { get; set; } = string.Empty;
    public long Size { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ContentType { get; set; } = string.Empty;
}
