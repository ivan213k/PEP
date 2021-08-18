using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Model.Deeplinks
{
    public class CreateDeeplinkDto
    {

        public int UserId { get; set; }
        public DateTime ExpiresDate { get; set; }
        public int SurveyId { get; set; }
        public int SentById { get; set; }

    }
}
