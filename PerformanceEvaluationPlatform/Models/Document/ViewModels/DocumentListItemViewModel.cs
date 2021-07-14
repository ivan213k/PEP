using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.ViewModels
{
    public class DocumentListItemViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DocumentTypeName { get; set; }
        public DateTime ValidToDate { get; set;}
        public string FileName { get; set;} 
    }
}
