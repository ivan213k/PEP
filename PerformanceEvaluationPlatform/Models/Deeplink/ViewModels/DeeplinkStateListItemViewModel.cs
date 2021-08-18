using PerformanceEvaluationPlatform.Application.Model.Deeplinks;

namespace PerformanceEvaluationPlatform.Models.Deeplink.ViewModels
{
    public class DeeplinkStateListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static DeeplinkStateListItemViewModel AsViewModel(this DeeplinkStateListItemDto dto)
        {
            return new DeeplinkStateListItemViewModel
            {
                Id = dto.Id,
                Name = dto.Title

            };
        }
    }
}
