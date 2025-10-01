using Arqom.Extensions.Storage.Abstractions;
using Arqom.Extensions.Storage.MinIo.Options;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

public class MinIoStorageAdapter : IStorageAdapter
{
    private readonly IMinioClient _client;
    private readonly MinIoStorageOptions _options;

    public MinIoStorageAdapter(IOptions<MinIoStorageOptions> options)
    {
        _options = options.Value;
        _client = new MinioClient()
            .WithEndpoint(_options.Endpoint)
            .WithCredentials(_options.AccessKey, _options.SecretKey)
            .Build();
    }

    private string? GetContentTypeFromFileName(string fileName)
    {
        fileName = Uri.UnescapeDataString(fileName);
        var provider = new FileExtensionContentTypeProvider();
        return provider.TryGetContentType(fileName, out var contentType) ? contentType : null;
    }

    public async Task<string> UploadAsync(Stream stream, string relativePath, CancellationToken cancellationToken = default)
    {
        if (stream == null || string.IsNullOrWhiteSpace(relativePath))
            throw new ArgumentException("Invalid arguments");

        if (stream.CanSeek)
            stream.Position = 0;

        var contentType = GetContentTypeFromFileName(relativePath) ?? "application/octet-stream";

        await _client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_options.BucketName)
            .WithObject(relativePath)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType(contentType), cancellationToken
        );

        return relativePath;
    }

    public async Task<Stream> DownloadAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        var ms = new MemoryStream();

        await _client.GetObjectAsync(
            new GetObjectArgs()
                .WithBucket(_options.BucketName)
                .WithObject(relativePath)
                .WithCallbackStream(stream => stream.CopyTo(ms)), cancellationToken
        );

        ms.Position = 0;
        return ms;
    }

    public async Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        await _client.RemoveObjectAsync(
            new RemoveObjectArgs()
                .WithBucket(_options.BucketName)
                .WithObject(relativePath), cancellationToken
        );
    }

    public async Task<bool> ExistsAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        try
        {
            await _client.StatObjectAsync(
                new StatObjectArgs()
                    .WithBucket(_options.BucketName)
                    .WithObject(relativePath), cancellationToken
            );
            return true;
        }
        catch (ObjectNotFoundException)
        {
            return false;
        }
    }

    public async Task<string> GetUrlAsync(string relativePath, TimeSpan? expireTime = null, CancellationToken cancellationToken = default)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(_options.BucketName)
            .WithObject(relativePath)
            .WithExpiry((int)(expireTime?.TotalSeconds ?? 3600));
        return await _client.PresignedGetObjectAsync(args);
    }

    public async Task<IEnumerable<string>> ListFilesAsync(string directoryPath, CancellationToken cancellationToken = default)
    {
        var files = new List<string>();

        await foreach (var item in _client.ListObjectsEnumAsync(
            new ListObjectsArgs()
                .WithBucket(_options.BucketName)
                .WithPrefix(directoryPath)
                .WithRecursive(true),
            cancellationToken))
        {
            files.Add(item.Key);
        }

        return files;
    }

    public async Task<FileMetadata> GetFileInfoAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        var stat = await _client.StatObjectAsync(
            new StatObjectArgs()
                .WithBucket(_options.BucketName)
                .WithObject(relativePath), cancellationToken
        );

        return new FileMetadata
        {
            Name = relativePath,
            ContentType = stat.ContentType,
            Size = stat.Size,
            CreatedOn = stat.LastModified,
        };
    }

    public async Task CopyAsync(string sourcePath, string destinationPath, CancellationToken cancellationToken = default)
    {
        await _client.CopyObjectAsync(
            new CopyObjectArgs()
                .WithBucket(_options.BucketName)
                .WithObject(destinationPath)
                .WithCopyObjectSource(new CopySourceObjectArgs()
                    .WithBucket(_options.BucketName)
                    .WithObject(sourcePath)), cancellationToken
        );
    }

    public async Task MoveAsync(string sourcePath, string destinationPath, CancellationToken cancellationToken = default)
    {
        await CopyAsync(sourcePath, destinationPath, cancellationToken);
        await DeleteAsync(sourcePath, cancellationToken);
    }
}