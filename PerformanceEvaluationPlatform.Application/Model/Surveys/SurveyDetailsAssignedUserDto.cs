using PerformanceEvaluationPlatform.Application.Model.Surveys.Enums;

namespace PerformanceEvaluationPlatform.Application.Model.Surveys
{
    public class SurveyDetailsAssignedUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SurveyAssignedUserStatus Status { get; set; }
    }
}
