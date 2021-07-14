using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.ViewModels
{
    public class DocumentDetailViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DocumentType { get; set; }
        public DateTime ValidTo { get; set; }
        public string FileName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastUpdatesBy { get; set; }
        public DateTime LastUpdatesAt { get; set; }
    }
}
