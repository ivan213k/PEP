using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
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
    public static partial class ViewModelMapperExtensions
    {
        public static CreateDeeplinkDto AsDto(this CreateDeeplinkRequestModel viewmodel)
        {
            return new CreateDeeplinkDto
            {
                UserId = viewmodel.UserId,
                SentById = viewmodel.SentById,
                SurveyId = viewmodel.SurveyId,
                ExpiresDate = viewmodel.ExpiresDate

            };
        }
    }

}
