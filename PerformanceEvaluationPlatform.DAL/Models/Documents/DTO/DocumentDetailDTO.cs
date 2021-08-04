using System;

namespace PerformanceEvaluationPlatform.DAL.Models.Documents.Dto
{
    public class DocumentDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public DateTime ValidToDate { get; set; }
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastUpdatesByFirstName { get; set; }
        public string LastUpdatesByLastName { get; set; }
        public DateTime? LastUpdatesAt { get; set; }

    }
}
