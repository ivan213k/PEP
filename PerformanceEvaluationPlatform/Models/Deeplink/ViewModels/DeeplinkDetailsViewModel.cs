using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Deeplink.ViewModels
{
    public class DeeplinkDetailsViewModel
    {
        public int Id { get; set; }
        public string SentTo { get; set; }
       // public int SentToId { get; set; }
        //public string ExspiresAt { get; set; }
        public string SentToEmail { get; set; }
        public DateTime SentAt { get; set; }
        public string SentBy { get; set; }

        public string State { get; set; }
        // public int StateId { get; set; }
        public DateTime ExpiresAt { get; set; }

        public string FormTemplateName { get; set; }
       // public int FormTemplateNameId { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static DeeplinkDetailsViewModel AsViewModel(this DeeplinkDetailsDto dto)
        {
            return new DeeplinkDetailsViewModel
            {
                Id = dto.Id,
                // wait user
              //  SentTo = $"{dto.SentTo.FirstName} { dto.SentTo.LastName }",
              //  SentToEmail = dto.SentTo.Email,
              //  SentBy = $"{dto.SentBy.FirstName} { dto.SentBy.LastName }",
                State = dto.StateName,
                ExpiresAt = dto.ExpiresAt,
                FormTemplateName = dto.FormTemplateName,
                SentAt = dto.SentAt

            };
        }
    }
}
