using PerformanceEvaluationPlatform.Models.FormData.Enums;
using System;

namespace PerformanceEvaluationPlatform.Models.FormData.ViewModels
{
    public class FormDataDetailViewModel
    {
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string Assignee { get; set; }
        public int AssigneeId { get; set; }
        public string Reviewer { get; set; }
        public int ReviewerId { get; set; }
        public StateEnum State { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string RecommendedLevel { get; set; }
        public int RecommendedLevelId { get; set; }
        public string Project { get; set; }
        public int ProjectId { get; set; }
        public string Team { get; set; }
        public int TeamId { get; set; }
        public string ExperienceInCompany { get; set; }
        public EnglishLevelEnum EnglishLevel { get; set; }
        public string Period { get; set; }
        public string CurrentPosition { get; set; }
    }
}
