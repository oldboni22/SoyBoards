namespace Common.Blob.Azure;

public class AzureBlobServiceOptions
{
    public const string SectionName = "Azure:Blob";
    
    public required string ContainerName { get; set; }
}