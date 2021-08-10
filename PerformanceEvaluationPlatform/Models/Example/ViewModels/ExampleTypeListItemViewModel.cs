using PerformanceEvaluationPlatform.Application.Model.Examples;

namespace PerformanceEvaluationPlatform.Models.Example.ViewModels
{
    public class ExampleTypeListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
	    public static ExampleTypeListItemViewModel AsViewModel(this ExampleTypeListItemDto dto)
	    {
            return new ExampleTypeListItemViewModel
            {
	            Id = dto.Id,
	            Title = dto.Title
            };
        }
    }
}
