using System.IO;
using System.Threading.Tasks;

namespace Common.Blob;

public interface IBlobService
{
    Task UploadFileAsync(Stream fileStream, string fileName, string contentType);
    
    Task<string?> GetFileLinkAsync(string fileName);
    
    Task<FileOutput?> GetFileAsync(string fileName);
    
    Task DeleteFileAsync(string fileName);     
}
