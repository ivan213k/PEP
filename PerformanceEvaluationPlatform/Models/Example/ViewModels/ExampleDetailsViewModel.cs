using PerformanceEvaluationPlatform.Application.Model.Examples;

namespace PerformanceEvaluationPlatform.Models.Example.ViewModels
{
    public class ExampleDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TypeName { get; set; }
        public string StateName { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
	    public static ExampleDetailsViewModel AsViewModel(this ExampleDetailsDto dto)
	    {
		    return new ExampleDetailsViewModel
		    {
			    Id = dto.Id,
			    Title = dto.Title,
			    StateName = dto.StateName,
			    TypeName = dto.TypeName
		    };
	    }
    }
}
