using System;
namespace PerformanceEvaluationPlatform.Models.Deeplink.VievModels
{


    public class DeeplinkListItemViewModel
    {
        public string SentTo { get; set; }
        public int SentToId { get; set; }
        public string ExspiresAt { get; set; }

        public string State { get; set; }
        public int StateId { get; set; }
        public string FormTemplateName { get; set; }
        public int FormTemplateNameId { get; set; }
        
    }
}
