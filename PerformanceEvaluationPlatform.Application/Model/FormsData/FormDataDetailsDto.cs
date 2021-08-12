using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.FormsData
{
    public class FormDataDetailsDto
    {
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string Assignee { get; set; }
        public int AssigneeId { get; set; }
        public string Reviewer { get; set; }
        public int ReviewerId { get; set; }
        public string State { get; set; }
        public int FormDataStateId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string RecommendedLevel { get; set; }
        public int RecommendedLevelId { get; set; }
        public string Project { get; set; }
        public int ProjectId { get; set; }
        public string Team { get; set; }
        public int TeamId { get; set; }
        public int ExperienceInCompany { get; set; }
        public string EnglishLevel { get; set; }
        public string Period { get; set; }
        public string CurrentPosition { get; set; }

        public IEnumerable<FormDataAnswersItemDto> Answers { get; set; }

    }
    public class FormDataAnswersItemDto
    {
        public string Skills { get; set; }
        public string Comment { get; set; }
        public string Assessment { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set; }
        public int Order { get; set; }
    }
}
