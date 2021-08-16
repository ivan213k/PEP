using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
using System;
namespace PerformanceEvaluationPlatform.Models.Deeplink.ViewModels
{


    public class DeeplinkListItemViewModel
    {
        public int Id { get; set; }
        public string SentTo { get; set; }
      //  public int SentToId { get; set; }
        //public string ExspiresAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public string State { get; set; }
      //  public int StateId { get; set; }
        public string FormTemplateName { get; set; }
      //  public int FormTemplateNameId { get; set; }
        
    }
    public static partial class ViewModelMapperExtensions
    {
        public static DeeplinkListItemViewModel AsViewModel(this DeeplinkListItemDto dto)
        {
            return new DeeplinkListItemViewModel
            {
                Id = dto.Id,
                SentTo = $"{dto.SentToFirstName} { dto.SentToLastName }",
                State = dto.State,
                ExpiresAt = dto.ExpireDate,
                FormTemplateName = dto.FormTemplate

            };
        }
    }
}
