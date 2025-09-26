using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PortalGalaxy.Common.Configuration;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.Services.Implementaciones;

public class FileUploader : IFileUploader
{
    private readonly StorageConfiguration _storageConfiguration;
    private readonly ILogger<FileUploader> _logger;

    public FileUploader(IOptions<AppSettings> options, ILogger<FileUploader> logger)
    {
        _storageConfiguration = options.Value.StorageConfiguration;
        _logger = logger;
    }

    public async Task<string> UploadFileAsync(string? base64String, string? fileName)
    {
        if (string.IsNullOrEmpty(base64String) || string.IsNullOrEmpty(fileName))
            return string.Empty;

        try
        {
            var client = new BlobServiceClient(_storageConfiguration.Path);
            var container = client.GetBlobContainerClient("talleres");

            var blob = container.GetBlobClient(fileName);

            await using var stream = new MemoryStream(Convert.FromBase64String(base64String));
            await blob.UploadAsync(stream, overwrite: true);

            _logger.LogInformation("Se subió correctamente el archivo a Azure Blob Storage");

            return $"{_storageConfiguration.PublicUrl}/{fileName}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al subir archivos a Azure Blob Storage {Message}", ex.Message);
            return string.Empty;
        }
    }
}