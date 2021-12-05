using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
using System;
using System.ComponentModel.DataAnnotations;

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
