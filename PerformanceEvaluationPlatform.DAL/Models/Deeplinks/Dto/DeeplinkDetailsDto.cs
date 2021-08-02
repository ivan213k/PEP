using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dto
{
    public class DeeplinkDetailsDto
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public DateTime SentAt { get; set; }
        public string StateName { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string FormTemplateName { get; set; }
        public DeeplinkUserRefDto SentTo { get; set; }
        public DeeplinkUserRefDto SentBy { get; set; }
    }
}
