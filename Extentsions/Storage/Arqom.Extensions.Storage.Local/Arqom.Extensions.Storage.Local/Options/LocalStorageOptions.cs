namespace Arqom.Extensions.Storage.Local.Options;

public class LocalStorageOptions
{
    public string BasePath { get; set; } = string.Empty; // مسیر ریشه برای ذخیره فایل
    public string BaseUrl { get; set; } = string.Empty;  // آدرس دسترسی (مثلاً http://localhost:5000/files)
}
