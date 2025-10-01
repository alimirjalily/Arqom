using Arqom.Extensions.Storage.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Arqom.Extensions.Storage.MinIo.Sample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IStorageAdapter _storage;

    public FileController(IStorageAdapter storage)
    {
        _storage = storage;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");

        using var stream = file.OpenReadStream();
        var path = await _storage.UploadAsync(stream, file.FileName);

        return Ok(new { path });
    }

    [HttpGet("download/{fileName}")]
    public async Task<IActionResult> Download(string fileName)
    {
        fileName = Uri.UnescapeDataString(fileName);
        var stream = await _storage.DownloadAsync(fileName);
        return File(stream, "application/octet-stream", fileName);
    }

    [HttpGet("exists/{fileName}")]
    public async Task<IActionResult> Exists(string fileName)
    {
        fileName = Uri.UnescapeDataString(fileName);
        var exists = await _storage.ExistsAsync(fileName);
        return Ok(new { exists });
    }

    [HttpGet("url/{fileName}")]
    public async Task<IActionResult> GetUrl(string fileName)
    {
        fileName = Uri.UnescapeDataString(fileName);
        var url = await _storage.GetUrlAsync(fileName, TimeSpan.FromMinutes(10));
        return Ok(new { url });
    }

    [HttpDelete("delete/{fileName}")]
    public async Task<IActionResult> Delete(string fileName)
    {
        fileName = Uri.UnescapeDataString(fileName);
        await _storage.DeleteAsync(fileName);
        return NoContent();
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListFiles([FromQuery] string? path, CancellationToken cancellationToken)
    {
        var files = await _storage.ListFilesAsync(path ?? string.Empty, cancellationToken);
        return Ok(files);
    }

    [HttpGet("FileInfo")]
    public async Task<IActionResult> GetFileInfo(string? relativePath, CancellationToken cancellationToken)
    {
        var fileInfo = await _storage.GetFileInfoAsync(relativePath ?? string.Empty, cancellationToken);
        return Ok(fileInfo);
    }

    [HttpPost("copy")]
    public async Task<IActionResult> Copy(string sourcePath, string destinationPath)
    {
        try
        {
            await _storage.CopyAsync(sourcePath, destinationPath);
            return Ok(new { message = $"Copied {sourcePath} → {destinationPath}" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("move")]
    public async Task<IActionResult> Move(string sourcePath, string destinationPath)
    {
        try
        {
            await _storage.MoveAsync(sourcePath, destinationPath);
            return Ok(new { message = $"Moved {sourcePath} → {destinationPath}" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}
