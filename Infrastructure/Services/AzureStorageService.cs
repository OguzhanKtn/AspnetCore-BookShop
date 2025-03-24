using Application.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public sealed class AzureStorageService : IAzureStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public AzureStorageService(IOptions<AzureStorageSettings> options)
    {
        _blobServiceClient = new BlobServiceClient(options.Value.ConnectionString);
        _containerName = options.Value.ContainerName;
    }
    public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {
        var blobContainer = _blobServiceClient.GetBlobContainerClient(_containerName);
        await blobContainer.CreateIfNotExistsAsync(PublicAccessType.Blob);
        var blobClient = blobContainer.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite: true);
        return blobClient.Uri.ToString();
    }
}
