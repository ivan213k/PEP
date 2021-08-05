using System;

namespace PerformanceEvaluationPlatform.Models.Document.ViewModels
{
    public class DocumentListItemViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentTypeName { get; set; }
        public DateTime ValidToDate { get; set;}
        public string FileName { get; set;} 
    }
}
