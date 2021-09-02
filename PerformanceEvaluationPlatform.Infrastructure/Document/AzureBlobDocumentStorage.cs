using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PerformanceEvaluationPlatform.Application.Interfaces.Documents;
using System;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Infrastructure.Document
{
    public class AzureBlobDocumentStorage : IDocumentStorage
    {
        private readonly BlobContainerClient _blobContainerClient;
        private const string ContainerName = "documentsblob";

        public  AzureBlobDocumentStorage(DocumentStorageConfigur conf)
        {
            var blobServiceClient = new BlobServiceClient(conf.ConnectionString);
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            _blobContainerClient.CreateIfNotExists();
                
            
        }
        public async Task Delete(string fileName)
        {
           // await _blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
            BlobClient client = _blobContainerClient.GetBlobClient(fileName);
            await client.DeleteIfExistsAsync();
        }

        public async Task Upload(string filePath,byte[] fileStream)
        {
           // await _blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
            BlobClient client = _blobContainerClient.GetBlobClient(filePath);
            BinaryData data = new BinaryData(fileStream);
            await client.UploadAsync(data);
            
        }

        public async Task UpdateFileInStorage(string oldPath, string newPath,byte[] fileStream)
        {
            //await _blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
            BlobClient client = _blobContainerClient.GetBlobClient(oldPath);
            await client.DeleteIfExistsAsync();
            var secondClient = _blobContainerClient.GetBlobClient(newPath);
            BinaryData data = new BinaryData(fileStream);
            await secondClient.UploadAsync(data);
        }

        public async Task<BlobFileDto> Download(string fileName)
        {
           // await _blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
            BlobClient client = _blobContainerClient.GetBlobClient(fileName);
            Response<BlobDownloadInfo> download = await client.DownloadAsync();
            return new BlobFileDto { Content = download.Value.Content, ContentType = download.Value.ContentType, FileName = fileName };
            
        }
    }
}
