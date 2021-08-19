using PerformanceEvaluationPlatform.Application.Model.Surveys;

namespace PerformanceEvaluationPlatform.Models.Survey.ViewModels
{
    public class SurveyStateListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static SurveyStateListItemViewModel AsViewModel(this SurveyStateListItemDto dto)
        {
            return new SurveyStateListItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
