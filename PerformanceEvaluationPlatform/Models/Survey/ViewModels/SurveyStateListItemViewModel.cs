using PerformanceEvaluationPlatform.Application.Model.Surveys;
using PerformanceEvaluationPlatform.Models.Shared;

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
        public static FilterDropDownItemViewModel AsFilterDropDownItemViewModel(this SurveyStateListItemDto dto)
        {
            return new FilterDropDownItemViewModel
            {
                Id = dto.Id,
                Value = dto.Name
            };
        }
    }
}
