using PerformanceEvaluationPlatform.Models.Survey.Enums;
using System.Text.Json.Serialization;

namespace PerformanceEvaluationPlatform.Models.Survey.ViewModels
{
    public class SurveyAssigneeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SurveyAssignedUserStatus Status { get; set; }
    }
}
