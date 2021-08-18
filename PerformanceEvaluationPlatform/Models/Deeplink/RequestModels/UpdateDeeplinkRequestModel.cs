using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
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
    public static partial class ViewModelMapperExtensions
    {
        public static UpdateDeeplinkDto AsDto(this UpdateDeeplinkRequestModel viewmodel)
        {
            return new UpdateDeeplinkDto
            {
                ExpiresAt = viewmodel.ExpiresAt
            };
        }
    }
}
