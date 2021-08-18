using PerformanceEvaluationPlatform.Domain.Shared;
using System;
namespace PerformanceEvaluationPlatform.Domain.Documents
{
    public class Document: IUpdatebleCreateable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public DateTime ValidToDate { get; set; }
        public string FileName { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; }
        public int? LastUpdatesById { get; set; }
        public DateTime? LastUpdatesAt { get; }
        public string MetaData { get; set; }

        //navigation prop
        public DocumentType DocumentType { get; set; }
        //public User User { get; set; }
        //public User CreatedBy { get; set; }
        //public User UpdatedBy { get; set; }
    }
}
