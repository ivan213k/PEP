using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Documents
{
    public interface IDocumentStorage
    {
        Task Upload(string filePath, byte[] fileStream);
        Task Delete(string fileName);
        Task<BlobFileDto> Download(string fileName);
        Task UpdateFileInStorage(string oldPath, string newPath, byte[] fileStream);
    }
}

