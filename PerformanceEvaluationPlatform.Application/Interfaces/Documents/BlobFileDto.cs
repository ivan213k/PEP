using System.IO;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Documents
{
    public class BlobFileDto
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
    
}
