namespace Application.Interfaces;

public interface IAzureStorageService
{
   Task<string> UploadImageAsync(Stream fileStream, string fileName);
}
