using System;

namespace PerformanceEvaluationPlatform.Application.Model.Documents
{
    public class UpdateDocumentDto
    {
        public int TypeId { get; set; }
        public DateTime ValidToDate { get; set; }
        public string FileName { get; set; }
        public int? LastUpdateById { get; set; }
        public string MetaData { get; set; }
    }
}
