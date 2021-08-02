using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Deeplink.RequestModels
{
    public class CreateDeeplinkRequestModel
    {

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime ExpiresDate { get; set; }

        [Required]
        public int SurveyId { get; set; }

        [Required]
        public int SentById { get; set; }

    }
}
