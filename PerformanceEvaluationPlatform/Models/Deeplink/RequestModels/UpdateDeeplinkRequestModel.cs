using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Deeplink.RequestModels
{
    public class UpdateDeeplinkRequestModel
    {


        [Required]
        public DateTime ExpiresAt { get; set; }

    }
}
