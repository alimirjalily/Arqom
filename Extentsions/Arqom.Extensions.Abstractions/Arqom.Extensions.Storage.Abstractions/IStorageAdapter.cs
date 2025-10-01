namespace Arqom.Extensions.Storage.Abstractions;

public interface IStorageAdapter
{
    // --- متدهای پایه (Core) ---

    /// <summary>
    /// آپلود فایل
    /// </summary>
    Task<string> UploadAsync(Stream content, string relativePath, CancellationToken cancellationToken = default);

    /// <summary>
    /// دانلود فایل به صورت Stream
    /// </summary>
    Task<Stream> DownloadAsync(string relativePath, CancellationToken cancellationToken = default);

    /// <summary>
    /// حذف فایل
    /// </summary>
    Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default);

    /// <summary>
    /// بررسی وجود فایل
    /// </summary>
    Task<bool> ExistsAsync(string relativePath, CancellationToken cancellationToken = default);


    // --- متدهای تکمیلی (Optional) ---

    /// <summary>
    /// برگرداندن URL یا لینک دسترسی به فایل
    /// اگر استوریج از pre-signed url پشتیبانی کنه میشه زمان انقضا هم داد.
    /// </summary>
    Task<string> GetUrlAsync(string relativePath, TimeSpan? expireTime = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// گرفتن لیست فایل‌ها در یک دایرکتوری (فولدر)
    /// </summary>
    Task<IEnumerable<string>> ListFilesAsync(string directoryPath, CancellationToken cancellationToken = default);

    /// <summary>
    /// گرفتن متادیتای فایل (سایز، تاریخ، ContentType و ...)
    /// </summary>
    Task<FileMetadata> GetFileInfoAsync(string relativePath, CancellationToken cancellationToken = default);

    /// <summary>
    /// کپی کردن فایل
    /// </summary>
    Task CopyAsync(string sourcePath, string destinationPath, CancellationToken cancellationToken = default);

    /// <summary>
    /// جابه‌جا کردن فایل (move = copy + delete)
    /// </summary>
    Task MoveAsync(string sourcePath, string destinationPath, CancellationToken cancellationToken = default);
}
