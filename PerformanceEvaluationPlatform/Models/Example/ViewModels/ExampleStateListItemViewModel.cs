using PerformanceEvaluationPlatform.Application.Model.Examples;

namespace PerformanceEvaluationPlatform.Models.Example.ViewModels
{
    public class ExampleStateListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
	    public static ExampleStateListItemViewModel AsViewModel(this ExampleStateListItemDto dto)
	    {
		    return new ExampleStateListItemViewModel
		    {
			    Id = dto.Id,
			    Name = dto.Title
		    };
	    }
    }
}
