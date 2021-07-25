using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dto
{
    public class DeeplinkDetailsDto
    {
        public int Id { get; set; }
        //public string SentTo { get; set; }

        public string SentToFirstName { get; set; }
        public string SentToSecondtName { get; set; }
        public string SentToEmail { get; set; }
        public DateTime SentAt { get; set; }
        public string SentBy { get; set; }
        public string StateName { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string FormTemplateName { get; set; }
    }
}
