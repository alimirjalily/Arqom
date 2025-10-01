namespace Arqom.Extensions.Storage.MinIo.Options;

public class MinIoStorageOptions
{
    public string Endpoint { get; set; } = string.Empty; // مثل http://localhost:9000
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = "my-bucket";
    public string BaseUrl { get; set; } = "https://localhost:9000/my-bucket";
}
