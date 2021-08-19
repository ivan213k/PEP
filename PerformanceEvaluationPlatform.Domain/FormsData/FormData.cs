using PerformanceEvaluationPlatform.Domain.Fields;
using PerformanceEvaluationPlatform.Domain.Surveys;
using System.Collections.Generic;


namespace PerformanceEvaluationPlatform.Domain.FormsData
{
    public class FormData
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int UserId { get; set; }
        public int FormDataStateId { get; set; }
        public FormDataState FormDataState { get; set; }

      //  public Users User { get; set; }
        public Survey Survey { get; set; }
        public ICollection<FieldData> FieldData { get; set; }
    }
}
