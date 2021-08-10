using PerformanceEvaluationPlatform.Application.Model.Examples;

namespace PerformanceEvaluationPlatform.Models.Example.ViewModels
{
    public class ExampleListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static ExampleListItemViewModel AsViewModel(this ExampleListItemDto dto)
        {
            return new ExampleListItemViewModel
            {
                Id = dto.Id,
                State = dto.StateName,
                Type = dto.TypeName,
                Title = dto.Title
            };
        }
    }
}
