using PerformanceEvaluationPlatform.Domain.Deeplinks;
using PerformanceEvaluationPlatform.Domain.FormsData;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Domain.Surveys
{
    public class Survey
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public int FormTemplateId { get; set; }
        public int AssigneeId { get; set; }
        public int SupervisorId { get; set; }
        public int RecommendedLevelId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Summary { get; set; }

        public SurveyState SurveyState { get; set; }
        public Level RecomendedLevel { get; set; }

        //public User Asignee { get; set; }
        //public User Supervisor { get; set; }
        //public FormTemplate FormTemplate { get; set; }

        public ICollection<Deeplink> DeepLinks { get; set; }
        public ICollection<FormData> FormData { get; set; }
    }
}
