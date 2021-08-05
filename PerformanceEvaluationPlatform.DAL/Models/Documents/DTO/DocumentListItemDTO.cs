using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Documents.Dto
{
    public class DocumentListItemDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentTypeName { get; set; }
        public DateTime ValidToDate { get; set; }
        public string FileName { get; set; }
    }
}
