using Arqom.Extensions.Storage.Abstractions;
using Arqom.Extensions.Storage.Local.Options;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

namespace Arqom.Extensions.Storage.Local.Services;

public class LocalStorageAdapter : IStorageAdapter
{
    private readonly LocalStorageOptions _options;

    public LocalStorageAdapter(IOptions<LocalStorageOptions> options)
    {
        _options = options.Value;
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_options.BasePath, fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        using var file = File.Create(filePath);
        await fileStream.CopyToAsync(file, cancellationToken);

        return fileName;
    }

    public Task<Stream> DownloadAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_options.BasePath, fileName);
        Stream stream = File.OpenRead(filePath);
        return Task.FromResult(stream);
    }

    public Task DeleteAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_options.BasePath, fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
        return Task.CompletedTask;
    }

    public Task<string> GetUrlAsync(string fileName)
    {
        return Task.FromResult($"{_options.BaseUrl}/{fileName}");
    }

    public Task<bool> ExistsAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_options.BasePath, relativePath);
        return Task.FromResult(File.Exists(filePath));
    }

    public Task<string> GetUrlAsync(string relativePath, TimeSpan? expireTime = null, CancellationToken cancellationToken = default)
    {
        // در حالت لوکال expireTime کاربرد نداره
        return Task.FromResult($"{_options.BaseUrl}/{relativePath.Replace("\\", "/")}");
    }

    public Task<IEnumerable<string>> ListFilesAsync(string directoryPath, CancellationToken cancellationToken = default)
    {
        var dirPath = Path.Combine(_options.BasePath, directoryPath);

        if (!Directory.Exists(dirPath))
            return Task.FromResult(Enumerable.Empty<string>());

        var files = Directory.GetFiles(dirPath)
                             .Select(Path.GetFileName)!
                             .ToList();

        return Task.FromResult<IEnumerable<string>>(files);
    }

    public Task<FileMetadata> GetFileInfoAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_options.BasePath, relativePath);

        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found", relativePath);

        var info = new FileInfo(filePath);

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(info.Name, out var contentType))
        {
            contentType = "application/octet-stream"; // fallback
        }

        var metadata = new FileMetadata
        {
            Name = info.Name,
            Size = info.Length,
            CreatedOn = info.CreationTimeUtc,
            ContentType = contentType
        };

        return Task.FromResult(metadata);
    }

    public Task CopyAsync(string sourcePath, string destinationPath, CancellationToken cancellationToken = default)
    {
        var src = Path.Combine(_options.BasePath, sourcePath);
        var dest = Path.Combine(_options.BasePath, destinationPath);

        if (!File.Exists(src))
            throw new FileNotFoundException("Source file not found", sourcePath);

        Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
        File.Copy(src, dest, overwrite: true);

        return Task.CompletedTask;
    }

    public Task MoveAsync(string sourcePath, string destinationPath, CancellationToken cancellationToken = default)
    {
        var src = Path.Combine(_options.BasePath, sourcePath);
        var dest = Path.Combine(_options.BasePath, destinationPath);

        if (!File.Exists(src))
            throw new FileNotFoundException("Source file not found", sourcePath);

        Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
        File.Move(src, dest, overwrite: true);

        return Task.CompletedTask;
    }
}