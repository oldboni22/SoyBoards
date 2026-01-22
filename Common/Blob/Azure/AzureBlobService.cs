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

    public void EnsureStorageExists()
    {
        _blobContainerClient.CreateIfNotExists();
    }

    public Task UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var client = _blobContainerClient.GetBlobClient(fileName);

        var uploadOptions = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType,
            }
        };
        
        return client.UploadAsync(fileStream, uploadOptions);
    }

    public async Task<string?> GetFileLinkAsync(string fileName)
    {
        var client = _blobContainerClient.GetBlobClient(fileName);
        
        if (!await client.ExistsAsync())
        {
            return null;
        }  
        
        var sasUri = client.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(5));
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
