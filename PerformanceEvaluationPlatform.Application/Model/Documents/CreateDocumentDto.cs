using System;

namespace PerformanceEvaluationPlatform.Application.Model.Documents
{
    public class CreateDocumentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public DateTime ValidToDate { get; set; }
        public string FileName { get; set; }
        public int CreatedById { get; set; }
        public string MetaDate { get; set; }
    }
}
