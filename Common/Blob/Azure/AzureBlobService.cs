using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;

namespace Common.Blob.Azure;

public class AzureBlobService(BlobServiceClient blobServiceClient, IOptions<AzureBlobServiceOptions> options) : IBlobService
{
    private readonly BlobContainerClient _blobContainerClient = blobServiceClient.GetBlobContainerClient(options.Value.ContainerName);

    public Task EnsureStorageExists()
    {
        _blobContainerClient.CreateIfNotExists();
        return Task.CompletedTask;
    }

    public async Task<bool> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var client = _blobContainerClient.GetBlobClient(fileName);

        if (await client.ExistsAsync())
        {
            return false;
        }
        
        var uploadOptions = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType,
            }
        };
        
        await client.UploadAsync(fileStream, uploadOptions);
        return true;
    }

    public async Task<string?> GetFileLinkAsync(string fileName)
    {
        var client = _blobContainerClient.GetBlobClient(fileName);
        
        if (!await client.ExistsAsync())
        {
            return null;
        }  
        
        var sasUri = client.GenerateSasUri(
            BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(AzureBlobConstants.MinutesToLoadFile));
        
        return sasUri.AbsoluteUri;
    }

    public async Task<FileOutput?> GetFileAsync(string fileName)
    {
        var client = _blobContainerClient.GetBlobClient(fileName);

        if (!await client.ExistsAsync())
        {
            return null;
        }    
        
        var downloadResponse = await client.DownloadStreamingAsync();
        
        var contentType = downloadResponse.Value.Details.ContentType;
        
        return new FileOutput
        {
            Content = downloadResponse.Value.Content,
            ContentType = contentType
        };
    }

    public Task DeleteFileAsync(string fileName)
    {
        var client = _blobContainerClient.GetBlobClient(fileName);
        
        return client.DeleteIfExistsAsync();
    }
}
