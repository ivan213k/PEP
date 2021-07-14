using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.RequestModels
{
    public class RequestAddDocumentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public DateTime ValidToDate { get; set; }
        public string FileName { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedByAt { get; set; }
        public string MetaDate { get; set; }
    }
}
