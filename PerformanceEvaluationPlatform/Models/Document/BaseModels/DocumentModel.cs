using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.BaseModels
{
    //Created on Database documentation with some modification
    public class DocumentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public DateTime ValidToDate { get; set; }
        public string FileName { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? LastUpdateById { get; set; }
        public DateTime? LastUpdatesAt { get; set; }
        public string MetaData { get; set; }
    }
}
